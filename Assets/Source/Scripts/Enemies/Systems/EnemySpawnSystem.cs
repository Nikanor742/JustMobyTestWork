using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Scripts.Configs;
using Source.Scripts.Enemies.Factories;
using Source.Scripts.Game.Views;
using Random = UnityEngine.Random;

namespace Source.Scripts.Enemies.Systems
{
    public sealed class EnemySpawnSystem : IDisposable
    {
        private readonly LevelView _levelView;
        private readonly IEnemyFactory _enemyFactory;
        private readonly EnemyConfigSO _enemyConfig;
        
        private readonly List<EnemySystem> _enemies;
        
        private readonly CancellationTokenSource _cancellationTokenSource;
        
        public EnemySpawnSystem(LevelView levelView, IEnemyFactory enemyFactory, EnemyConfigSO enemyConfig)
        {
            _levelView = levelView;
            _enemyFactory = enemyFactory;
            _enemyConfig = enemyConfig;

            _enemies = new List<EnemySystem>(10);
            
            _cancellationTokenSource = new CancellationTokenSource();
        }
        
        public void Initialize()
        {
            SpawnRoutine().Forget();
        }

        private async UniTask SpawnRoutine()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                if (_enemies.Count < _enemyConfig.MaxEnemyCount)
                {
                    var enemyView = _enemyFactory.Create(_enemyConfig, 
                        _levelView.EnemySpawnPoints[Random.Range(0, _levelView.EnemySpawnPoints.Length)]);
                    var enemyController = new EnemySystem(_enemyConfig);
                    enemyController.Initialize(enemyView).Forget();
                    
                    _enemies.Add(enemyController);
                }
                else
                {
                    foreach (var e in _enemies)
                    {
                        if(e.EnemyIsDead)
                            e.Respawn(_enemyFactory.Create(_enemyConfig, 
                                _levelView.EnemySpawnPoints[Random.Range(0, _levelView.EnemySpawnPoints.Length)]));
                    }
                }

                await UniTask.WaitForSeconds(_enemyConfig.SpawnDelay,
                    cancellationToken: _cancellationTokenSource.Token);
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}