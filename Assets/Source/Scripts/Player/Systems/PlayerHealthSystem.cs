using System;
using R3;
using Source.Scripts.Configs;
using Source.Scripts.Player.Models;
using Source.Scripts.Save;
using Source.Scripts.Upgrades;
using Source.Scripts.Upgrades.Enums;

namespace Source.Scripts.Player.Systems
{
    public sealed class PlayerHealthSystem : IDisposable
    {
        private readonly PlayerModel _playerModel;
        private readonly PlayerConfigSO _playerConfig;
        private readonly IUpgradeModificator _upgradeModificator;
        
        private readonly CompositeDisposable _disposables = new ();
        
        public PlayerHealthSystem(
            PlayerModel playerModel,
            PlayerConfigSO  playerConfig,
            IUpgradeModificator upgradeModificator)
        {
            _playerModel = playerModel;
            _playerConfig = playerConfig;
            _upgradeModificator = upgradeModificator;
        }

        public void Initialize()
        {
            RefreshHealth();
            
            SaveExtension.player.UpgradeStats[EUpgradeType.Health].OnChangeEvent
                .Subscribe(_ => RefreshHealth())
                .AddTo(_disposables);
        }

        private void RefreshHealth()
        {
            _playerModel.MaxHealth.Value = _upgradeModificator.
                GetValue(EUpgradeType.Health, 
                    _playerConfig.BaseHealth, 
                    SaveExtension.player.UpgradeStats[EUpgradeType.Health].Value);
            
            _playerModel.CurrentHealth.Value = _playerModel.MaxHealth.Value;
        }
        
        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}