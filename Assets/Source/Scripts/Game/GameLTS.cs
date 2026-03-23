using Source.Scripts.Configs;
using Source.Scripts.Enemies.Factories;
using Source.Scripts.Enemies.Systems;
using Source.Scripts.Game.Models;
using Source.Scripts.Game.Systems;
using Source.Scripts.Game.Views;
using Source.Scripts.Player.Models;
using Source.Scripts.Player.Systems;
using Source.Scripts.Player.Views;
using Source.Scripts.Upgrades;
using Source.Scripts.Upgrades.Models;
using Source.Scripts.Upgrades.Systems;
using Source.Scripts.Weapons.Factories;
using Source.Scripts.Localization;
using Source.Scripts.Tutor;
using Source.Scripts.Tutor.Systems;
using Unity.AI.Navigation;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Source.Scripts.Game
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
        [SerializeField] private LocalizationConfigSO _localizationConfig;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerConfig);
            builder.RegisterInstance(_gameConfig);
            builder.RegisterInstance(_weaponConfigs);
            builder.RegisterInstance(_enemyConfig);
            builder.RegisterInstance(_upgradeConfigs);
            builder.RegisterInstance(_localizationConfig);
            
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
            builder.Register<UpgradesSystem>(Lifetime.Scoped);
            builder.Register<PlayerUISystem>(Lifetime.Scoped);
            builder.Register<TutorSystem>(Lifetime.Scoped);
            builder.Register<LocalizationService>(Lifetime.Scoped);
            
            builder.RegisterEntryPoint<GameInitSystem>();
        }
    }
}