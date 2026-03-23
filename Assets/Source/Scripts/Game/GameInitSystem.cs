using System;
using Source.Scripts.Enemies.Systems;
using Source.Scripts.Extensions;
using Source.Scripts.Game.Systems;
using Source.Scripts.Localization;
using Source.Scripts.Player.Systems;
using Source.Scripts.Save;
using Source.Scripts.Tutor.Systems;
using Source.Scripts.Upgrades.Systems;
using UnityEngine;
using VContainer.Unity;

namespace Source.Scripts.Game
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
        private readonly UpgradesSystem _upgradesSystem;
        private readonly PlayerUISystem _playerUISystem;
        private readonly TutorSystem _tutorSystem;
        
        private readonly LocalizationService _localizationService;
        
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
            UpgradesSystem upgradesSystem,
            PlayerHealthSystem playerHealthSystem,
            PlayerUISystem playerUISystem,
            TutorSystem tutorSystem,
            LocalizationService localizationService
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
            _upgradesSystem = upgradesSystem;
            _playerHealthSystem = playerHealthSystem;
            _playerUISystem = playerUISystem;
            _tutorSystem = tutorSystem;
            
            _localizationService = localizationService;
        }

        public void Initialize()
        {
            Application.targetFrameRate = 60;
            SaveExtension.Override();
            
            _gameActions.Enable();
            
            _localizationService.Initialize();
            _playerInputSystem.Initialize(_gameActions);
            _gameInputSystem.Initialize(_gameActions);
            _levelSpawnSystem.Initialize();
            _playerSpawnSystem.Initialize();
            _playerControlSystem.Initialize();
            _playerWeaponSystem.Initialize();
            _playerWeaponSystem.SelectWeapon(0);
            _enemySpawnSystem.Initialize();
            _upgradesSystem.Initialize();
            _upgradesUISystem.Initialize();
            _playerHealthSystem.Initialize();
            _playerUISystem.Initialize();
            _tutorSystem.Initialize();
            
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
