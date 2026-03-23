using Source.Scripts.Enemies;
using Source.Scripts.Factories;
using Source.Scripts.Interfaces;
using Source.Scripts.Models;
using Source.Scripts.Systems;
using Source.Scripts.Views;
using Source.Scripts.Scriptable;
using Source.Scripts.Upgrades;
using Unity.AI.Navigation;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.DI
{
    public sealed class GameLTS : LifetimeScope
    {
        [Header("Prefabs")]
        [SerializeField] private PlayerView _playerViewPrefab;
        [SerializeField] private LevelView _levelViewPrefab;
        [SerializeField] private NavMeshSurface _navMeshSurfacePrefab;
        [SerializeField] private GameCanvasView _gameCanvasPrefab;

        [Header("Configs")] 
        [SerializeField] private PlayerConfigSO _playerConfig;
        [SerializeField] private GameConfigSO _gameConfig;
        [SerializeField] private WeaponConfigSO[] _weaponConfigs;
        [SerializeField] private EnemyConfigSO _enemyConfig;
        [SerializeField] private UpgradesConfigSO[] _upgradeConfigs;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerConfig);
            builder.RegisterInstance(_gameConfig);
            builder.RegisterInstance(_weaponConfigs);
            builder.RegisterInstance(_enemyConfig);
            builder.RegisterInstance(_upgradeConfigs);
            
            builder.RegisterComponentInNewPrefab(_levelViewPrefab, Lifetime.Scoped);
            builder.RegisterComponentInNewPrefab(_playerViewPrefab, Lifetime.Scoped);
            builder.RegisterComponentInNewPrefab(_navMeshSurfacePrefab, Lifetime.Scoped);
            builder.RegisterComponentInNewPrefab(_gameCanvasPrefab, Lifetime.Scoped);

            builder.RegisterComponentInHierarchy<Camera>();
            
            builder.Register<GameStateModel>(Lifetime.Scoped);
            builder.Register<PlayerModel>(Lifetime.Scoped);
            builder.Register<UpgradeWindowModel>(Lifetime.Scoped);
            builder.Register<UpgradeSessionModel>(Lifetime.Scoped);
            
            builder.Register<IWeaponFactory, WeaponFactory>(Lifetime.Scoped);
            builder.Register<IBulletFactory, BulletFactory>(Lifetime.Scoped);
            builder.Register<IEnemyFactory, EnemyFactory>(Lifetime.Scoped);
            
            builder.Register<IUpgradeModificator, PercentUpgradeModificator>(Lifetime.Scoped);
            
            builder.Register<PlayerInputSystem>(Lifetime.Scoped);
            builder.Register<GameInputSystem>(Lifetime.Scoped);
            builder.Register<LevelSpawnSystem>(Lifetime.Scoped);
            builder.Register<PlayerHealthSystem>(Lifetime.Scoped);
            builder.Register<PlayerSpawnSystem>(Lifetime.Scoped);
            builder.Register<PlayerControlSystem>(Lifetime.Scoped);
            builder.Register<PlayerWeaponSystem>(Lifetime.Scoped);
            builder.Register<EnemySpawnSystem>(Lifetime.Scoped);
            builder.Register<UpgradesUISystem>(Lifetime.Scoped);
            builder.Register<PlayerUpgradesSystem>(Lifetime.Scoped);
            builder.Register<PlayerUISystem>(Lifetime.Scoped);
            
            builder.RegisterEntryPoint<GameInitSystem>();
        }
    }
}