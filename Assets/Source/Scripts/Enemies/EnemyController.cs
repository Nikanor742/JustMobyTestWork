using System;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public sealed class EnemyController : IDisposable
    {
        private readonly EnemyView _enemyView;
        private readonly EnemyModel _enemyModel;
        private readonly EnemyConfigSO _enemyConfig;
        
        private readonly EnemyWaitState _waitState;
        private readonly EnemyMoveState _moveState;
        
        private readonly CompositeDisposable _disposables = new();

        private readonly CancellationTokenSource _cancellationToken;
        
        public EnemyController(EnemyView enemyView, EnemyConfigSO enemyConfig)
        {
            _enemyView = enemyView;
            _enemyModel = new EnemyModel();
            _enemyConfig = enemyConfig;
            _waitState = new EnemyWaitState(_enemyView, _enemyModel, _enemyConfig);
            _moveState = new EnemyMoveState(_enemyView, _enemyModel, _enemyConfig);

            _cancellationToken = new CancellationTokenSource();
            
            Initialize(_cancellationToken).Forget();
        }

        private async UniTask Initialize(CancellationTokenSource cancellationToken)
        {
            await UniTask.WaitForEndOfFrame(cancellationToken : cancellationToken.Token);

            _enemyModel.MaxHp = _enemyConfig.MaxHp;
            _enemyModel.CurrentHp.Value = _enemyConfig.MaxHp;
            
            _enemyModel.CurrentHp
                .Subscribe(_ => _enemyView.SetHealthNormalized(_enemyModel.HealthNormalized))
                .AddTo(_disposables);
            
            _enemyModel.CurrentState = _moveState;
            _enemyModel.CurrentState.Enter();

            _enemyView.TakeDamageProvider.OnTakeDamageEvent
                .Subscribe(TakeDamage)
                .AddTo(_disposables);

            Observable.EveryUpdate()
                .Subscribe(_ => Tick())
                .AddTo(_disposables);
        }

        private void TakeDamage(float damage)
        {
            _enemyModel.ApplyDamage(damage);
            
        }

        private void Tick()
        {
            if (_enemyModel.CurrentState == null)
                return;
            
            var isCompleted = _enemyModel.CurrentState.Tick();
            
            if(!isCompleted)
                return;
            
            if(_enemyModel.CurrentState == _moveState)
                ChangeState(_waitState);
            else if (_enemyModel.CurrentState == _waitState)
                ChangeState(_moveState);
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