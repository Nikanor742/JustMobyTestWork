using Source.Data;
using Source.Scripts.Enemies;
using Source.Scripts.Enums;
using UnityEngine;
using VContainer.Unity;

namespace Source.Scripts.Systems
{
    public sealed class GameInitSystem : IInitializable
    {
        private readonly PlayerInputSystem _playerInputSystem;
        private readonly LevelSpawnSystem _levelSpawnSystem;
        private readonly PlayerSpawnSystem _playerSpawnSystem;
        private readonly PlayerControlSystem _playerControlSystem;
        private readonly PlayerWeaponSystem _playerWeaponSystem;

        private readonly EnemySpawnSystem _enemySpawnSystem;
        
        public GameInitSystem(
            PlayerInputSystem playerInputSystem, 
            LevelSpawnSystem levelSpawnSystem,
            PlayerSpawnSystem playerSpawnSystem,
            PlayerControlSystem playerControlSystem,
            PlayerWeaponSystem playerWeaponSystem,
            EnemySpawnSystem enemySpawnSystem)
        {
            _playerInputSystem = playerInputSystem;
            _levelSpawnSystem = levelSpawnSystem;
            _playerSpawnSystem = playerSpawnSystem;
            _playerControlSystem =  playerControlSystem;
            _playerWeaponSystem = playerWeaponSystem;
            _enemySpawnSystem = enemySpawnSystem;
        }

        public void Initialize()
        {
            Application.targetFrameRate = 60;
            SaveExtension.Override();
            
            _playerInputSystem.Initialize();
            _levelSpawnSystem.Initialize();
            _playerSpawnSystem.Initialize();
            _playerControlSystem.Initialize();
            _playerWeaponSystem.Initialize();
            
            _playerWeaponSystem.SelectWeapon(0);

            _enemySpawnSystem.Initialize();
        }
    }
}
