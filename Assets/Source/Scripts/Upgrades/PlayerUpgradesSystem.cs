using System;
using System.Collections.Generic;
using R3;
using Source.Data;
using Source.Scripts.Enums;
using Source.Scripts.Models;
using Source.Scripts.Scriptable;
using Source.Scripts.Upgrades;
using Source.Scripts.Views;
using Source.Skills;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Scripts.Systems
{
    public sealed class PlayerUpgradesSystem : IDisposable
    {
        private readonly UpgradesWindowView _upgradesWindowView;
        private readonly GameInputSystem _gameInputSystem;
        private readonly UpgradeSessionModel _upgradeSessionModel;
        private readonly GameStateModel _gameStateModel;
        
        private readonly Dictionary<EUpgradeType, UpgradesConfigSO> _upgradeConfigs = new();
        
        private readonly CompositeDisposable _disposables = new();
        
        public PlayerUpgradesSystem(
            GameCanvasView gameCanvasView, 
            UpgradeSessionModel upgradeSessionModel, 
            GameStateModel gameStateModel,
            UpgradesConfigSO[] upgradeConfigs)
        {
            _upgradesWindowView = gameCanvasView.UpgradesWindowView;
            _upgradeSessionModel = upgradeSessionModel;
            _gameStateModel = gameStateModel;
            
            foreach (var config in upgradeConfigs)
                _upgradeConfigs.Add(config.UpgradeType, config);
        }

        public void Initialize()
        {
            foreach (var s in _upgradeConfigs)
            {
                var skillView = Object.Instantiate
                    (_upgradesWindowView.UpgradeTemplate, _upgradesWindowView.UpgradeTemplate.transform.parent );
                
                skillView.gameObject.SetActive(true);
                skillView.SetUpgradeName(s.Value.UpgradeName);
                _upgradesWindowView.Upgrades.Add(s.Key ,skillView);
            }
            
            foreach (var config in _upgradeConfigs)
            {
                _upgradeSessionModel.Skills.Add(config.Key, new SkillSessionModel());
            }
            
            foreach (var upgrade in _upgradesWindowView.Upgrades)
            {
                upgrade.Value.OnUpgradeButtonClicked
                    .Subscribe(view =>
                    {
                        OnUpgradeButtonClicked(upgrade.Key, view);
                    })
                    .AddTo(_disposables);
            }
            
            _gameStateModel.UpgradesIsOpen
                .Subscribe(OnUpgradesChangeState)
                .AddTo(_disposables);
            
            _upgradesWindowView.OnApplyButtonClicked
                .Subscribe(_=> OnApplyButtonClicked())
                .AddTo(_disposables);

            if (SaveExtension.player.UpgradeStats.Count == 0)
            {
                foreach (var config in _upgradeConfigs)
                {
                    SaveExtension.player.UpgradeStats.Add(config.Key, new SaveProperty<int>());
                }
            }
        }

        private void OnUpgradesChangeState(bool upgradeIsOpen)
        {
            if (upgradeIsOpen)
            {
                _upgradeSessionModel.AvailableSkillPoints.Value = SaveExtension.player.SkillPoints.Value;
                _upgradeSessionModel.SpentSkillPoints = 0;
                
                foreach (var s in _upgradeSessionModel.Skills)
                {
                    s.Value.MaxLevel.Value = _upgradeConfigs[s.Key].UpgradePercents.Length;
                    s.Value.CurrentLevel.Value = SaveExtension.player.UpgradeStats[s.Key].Value;
                    s.Value.AddedLevels = 0;
                }
            }
        }

        private void OnUpgradeButtonClicked(EUpgradeType upgradeType, UpgradeView upgradeView)
        {
            var currentLevel = _upgradeSessionModel.Skills[upgradeType].CurrentLevel.Value;
            var config = _upgradeConfigs[upgradeType];
            var maxLevel = config.UpgradePercents.Length;
            
            if (_upgradeSessionModel.AvailableSkillPoints.Value > 0)
            {
                if (currentLevel < maxLevel)
                {
                    _upgradeSessionModel.AvailableSkillPoints.Value--;
                    _upgradeSessionModel.SpentSkillPoints++;
                    _upgradeSessionModel.Skills[upgradeType].CurrentLevel.Value++;
                    _upgradeSessionModel.Skills[upgradeType].AddedLevels++;
                    _upgradeSessionModel.OnSuccessUpgradeEvent?.OnNext(Unit.Default);
                }
            }
        }

        private void OnApplyButtonClicked()
        {
            if (_upgradeSessionModel.SpentSkillPoints > 0)
            {
                SaveExtension.player.SkillPoints.Value -= _upgradeSessionModel.SpentSkillPoints;
                foreach (var s in _upgradeSessionModel.Skills)
                {
                    var playerUpgradeStat = SaveExtension.player.UpgradeStats[s.Key];
                    playerUpgradeStat.Value += s.Value.AddedLevels;
                }
                SaveExtension.SaveData();
            }
        }
        
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}