using Source.Scripts.Factories;
using Source.Scripts.Models;
using Source.Scripts.Systems;
using Source.Scripts.Views;
using Source.Scripts.Scriptable;
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

        [Header("Configs")] 
        [SerializeField] private PlayerConfigSO _playerConfig;
        [SerializeField] private GameConfigSO _gameConfig;
        [SerializeField] private WeaponConfigSO[] _weaponConfigs;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerConfig);
            builder.RegisterInstance(_gameConfig);
            builder.RegisterInstance(_weaponConfigs);
            
            builder.RegisterComponentInNewPrefab(_levelViewPrefab, Lifetime.Scoped);
            builder.RegisterComponentInNewPrefab(_playerViewPrefab, Lifetime.Scoped);
            
            builder.Register<PlayerModel>(Lifetime.Scoped);
            
            builder.Register<PlayerInputSystem>(Lifetime.Scoped);
            builder.Register<LevelSpawnSystem>(Lifetime.Scoped);
            builder.Register<PlayerSpawnSystem>(Lifetime.Scoped);
            builder.Register<PlayerControlSystem>(Lifetime.Scoped);
            builder.Register<WeaponFactory>(Lifetime.Scoped);
            builder.Register<PlayerWeaponSystem>(Lifetime.Scoped);
            
            builder.RegisterEntryPoint<GameInitSystem>();
        }
    }
}