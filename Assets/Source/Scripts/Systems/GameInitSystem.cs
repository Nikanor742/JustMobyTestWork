using System;
using Source.Data;
using Source.Scripts.Enemies;
using Source.Scripts.Views;
using UnityEngine;
using VContainer.Unity;

namespace Source.Scripts.Systems
{
    public sealed class GameInitSystem : IInitializable, IDisposable
    {
        private readonly PlayerInputSystem _playerInputSystem;
        private readonly GameInputSystem _gameInputSystem;
        private readonly LevelSpawnSystem _levelSpawnSystem;
        private readonly PlayerHealthSystem _playerHealthSystem;
        private readonly PlayerSpawnSystem _playerSpawnSystem;
        private readonly PlayerControlSystem _playerControlSystem;
        private readonly PlayerWeaponSystem _playerWeaponSystem;
        private readonly EnemySpawnSystem _enemySpawnSystem;
        private readonly UpgradesUISystem _upgradesUISystem;
        private readonly PlayerUpgradesSystem _playerUpgradesSystem;
        private readonly PlayerUISystem _playerUISystem;
        
        private GameActions _gameActions = new();
        
        public GameInitSystem(
            PlayerInputSystem playerInputSystem, 
            GameInputSystem gameInputSystem,
            LevelSpawnSystem levelSpawnSystem,
            PlayerSpawnSystem playerSpawnSystem,
            PlayerControlSystem playerControlSystem,
            PlayerWeaponSystem playerWeaponSystem,
            EnemySpawnSystem enemySpawnSystem,
            UpgradesUISystem upgradesUISystem,
            PlayerUpgradesSystem playerUpgradesSystem,
            PlayerHealthSystem playerHealthSystem,
            PlayerUISystem playerUISystem
            )
        {
            _playerInputSystem = playerInputSystem;
            _gameInputSystem = gameInputSystem;
            _levelSpawnSystem = levelSpawnSystem;
            _playerSpawnSystem = playerSpawnSystem;
            _playerControlSystem = playerControlSystem;
            _playerWeaponSystem = playerWeaponSystem;
            _enemySpawnSystem = enemySpawnSystem;
            _upgradesUISystem = upgradesUISystem;
            _playerUpgradesSystem = playerUpgradesSystem;
            _playerHealthSystem = playerHealthSystem;
            _playerUISystem = playerUISystem;
        }

        public void Initialize()
        {
            Application.targetFrameRate = 60;
            SaveExtension.Override();
            
            _gameActions.Enable();
            
            _playerInputSystem.Initialize(_gameActions);
            _gameInputSystem.Initialize(_gameActions);
            _levelSpawnSystem.Initialize();
            _playerSpawnSystem.Initialize();
            _playerControlSystem.Initialize();
            _playerWeaponSystem.Initialize();
            _playerWeaponSystem.SelectWeapon(0);
            _enemySpawnSystem.Initialize();
            _playerUpgradesSystem.Initialize();
            _upgradesUISystem.Initialize();
            _playerHealthSystem.Initialize();
            _playerUISystem.Initialize();
            
            //For drawing player data
            var dataDrawer = new GameObject("DataDrawer");
            dataDrawer.AddComponent<DataDrawer>();
        }

        public void Dispose()
        {
            _gameActions?.Disable();
            _gameActions?.Dispose();
        }
    }
}
