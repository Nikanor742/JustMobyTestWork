using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Lean.Pool;
using R3;
using Source.Scripts.Configs;
using Source.Scripts.Enemies.Models;
using Source.Scripts.Enemies.States;
using Source.Scripts.Enemies.Views;
using Source.Scripts.Save;
using UnityEngine;

namespace Source.Scripts.Enemies.Systems
{
    public sealed class EnemySystem : IDisposable
    {
        private readonly EnemyConfigSO _enemyConfig;
        private readonly EnemyModel _enemyModel;

        private CancellationTokenSource _cancellationToken;
        private readonly EnemyDeathState _deathState;

        private CompositeDisposable _disposables;
        private EnemyView _enemyView;
        private readonly EnemyMoveState _moveState;

        private readonly EnemyWaitState _waitState;
        
        public bool EnemyIsDead => _enemyModel.IsDead;

        public EnemySystem(EnemyConfigSO enemyConfig)
        {
            _enemyModel = new EnemyModel();
            _enemyConfig = enemyConfig;

            _waitState = new EnemyWaitState(_enemyModel, _enemyConfig);
            _moveState = new EnemyMoveState(_enemyModel, _enemyConfig);
            _deathState = new EnemyDeathState(_enemyModel, _enemyConfig);
        }

        public void Respawn(EnemyView enemyView)
        {
            Dispose();
            Initialize(enemyView).Forget();
        }

        public async UniTask Initialize(EnemyView enemyView)
        {
            _enemyView = enemyView;

            _cancellationToken = new CancellationTokenSource();
            _disposables = new CompositeDisposable();

            _waitState.SetView(_enemyView);
            _moveState.SetView(_enemyView);
            _deathState.SetView(_enemyView);

            _enemyModel.MaxHp = _enemyConfig.MaxHp;
            _enemyModel.CurrentHp.Value = _enemyConfig.MaxHp;
            _enemyModel.IsDead = false;
            _enemyModel.DeathTime = 0;

            _enemyView.SetHealthNormalized(_enemyModel.HealthNormalized);

            _enemyView.TakeDamageProvider.OnTakeDamageEvent
                .Subscribe(TakeDamage)
                .AddTo(_disposables);

            Observable.EveryUpdate()
                .Subscribe(_ => Tick())
                .AddTo(_disposables);

            _enemyModel.CurrentHp
                .Subscribe(_ => _enemyView.SetHealthNormalized(_enemyModel.HealthNormalized))
                .AddTo(_disposables);

            await UniTask.WaitForEndOfFrame(_cancellationToken.Token);

            _enemyModel.CurrentState = _moveState;
            _enemyModel.CurrentState.Enter();
        }

        private void TakeDamage(TakeDamageModel damageModel)
        {
            if (_enemyModel.CurrentHp.Value > 0)
            {
                _enemyModel.ApplyDamage(damageModel.Damage);
                if (_enemyModel.CurrentHp.Value <= 0)
                {
                    ChangeState(_deathState);
                    SaveExtension.player.SkillPoints.Value += 1;
                    SaveExtension.SaveData();
                }
            }

            var takeDamageFX = LeanPool.Spawn(_enemyConfig.BloodPrefab, damageModel.HitPoint, Quaternion.identity);
            LeanPool.Despawn(takeDamageFX, 1f);
        }

        private void Tick()
        {
            if (_enemyModel.CurrentState == null)
                return;

            var isCompleted = _enemyModel.CurrentState.Tick();

            if (!isCompleted)
                return;

            //Change states logic
            if (_enemyModel.CurrentState == _moveState)
            {
                ChangeState(_waitState);
            }
            else if (_enemyModel.CurrentState == _waitState)
            {
                ChangeState(_moveState);
            }
            else if (_enemyModel.CurrentState == _deathState && !_enemyModel.IsDead)
            {
                _enemyModel.IsDead = true;
                LeanPool.Despawn(_enemyView);
            }
        }

        private void ChangeState(IEnemyState newState)
        {
            _enemyModel.CurrentState?.Exit();
            _enemyModel.CurrentState = newState;
            _enemyModel.CurrentState.Enter();
        }
        
        public void Dispose()
        {
            _disposables?.Dispose();
            _cancellationToken.Cancel();
            _cancellationToken?.Dispose();
        }
    }
}