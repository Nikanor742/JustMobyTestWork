using System;
using R3;
using Source.Data;
using Source.Scripts.Enums;
using Source.Scripts.Interfaces;
using Source.Scripts.Models;
using Source.Scripts.Scriptable;
using UnityEngine;

namespace Source.Scripts.Systems
{
    public sealed class PlayerHealthSystem : IDisposable
    {
        private readonly PlayerModel _playerModel;
        private readonly PlayerConfigSO _playerConfig;
        private readonly IUpgradeModificator _upgradeModificator;
        private readonly UpgradeSessionModel _upgradeSessionModel;
        
        private readonly CompositeDisposable _disposables = new ();
        
        public PlayerHealthSystem(
            PlayerModel playerModel,
            PlayerConfigSO  playerConfig,
            IUpgradeModificator upgradeModificator,
            UpgradeSessionModel upgradeSessionModel)
        {
            _playerModel = playerModel;
            _playerConfig = playerConfig;
            _upgradeModificator = upgradeModificator;
            _upgradeSessionModel = upgradeSessionModel;
        }

        public void Initialize()
        {
            RefreshHealth();

            _upgradeSessionModel.OnSuccessUpgradeEvent
                .Subscribe(_ => RefreshHealth())
                .AddTo(_disposables);
        }

        private void RefreshHealth()
        {
            Debug.Log("Refreshing health");
            _playerModel.MaxHealth.Value = _upgradeModificator.
                GetValue(EUpgradeType.Health, 
                    _playerConfig.BaseHealth, 
                    SaveExtension.player.UpgradeStats[EUpgradeType.Health].Level);
            
            _playerModel.CurrentHealth.Value = _playerModel.MaxHealth.Value;
        }
        
        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}