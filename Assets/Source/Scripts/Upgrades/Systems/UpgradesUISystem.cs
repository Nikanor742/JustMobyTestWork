using System;
using System.Collections.Generic;
using R3;
using Source.Scripts.Configs;
using Source.Scripts.Game.Models;
using Source.Scripts.Game.Systems;
using Source.Scripts.Game.Views;
using Source.Scripts.Localization;
using Source.Scripts.Save;
using Source.Scripts.Upgrades.Enums;
using Source.Scripts.Upgrades.Models;
using Source.Scripts.Upgrades.Views;
using UnityEngine;

namespace Source.Scripts.Upgrades.Systems
{
    public sealed class UpgradesUISystem : IDisposable
    {
        private readonly GameInputSystem _gameInputSystem;
        private readonly UpgradesWindowView _upgradesWindowView;
        private readonly UpgradeWindowModel _upgradeWindowModel;
        private readonly UpgradeSessionModel _upgradeSessionModel;
        private readonly GameStateModel _gameStateModel;
        private readonly LocalizationService _localizationService;
        
        private readonly Dictionary<EUpgradeType, UpgradesConfigSO> _upgradeConfigs = new();
        
        private readonly CompositeDisposable _disposables = new ();
        
        public UpgradesUISystem(
            GameInputSystem gameInputSystem,
            GameCanvasView  gameCanvasView, 
            GameStateModel gameStateModel,
            UpgradeSessionModel upgradeSessionModel,
            UpgradeWindowModel upgradeWindowModel,
            UpgradesConfigSO[] upgradeConfigs,
            LocalizationService localizationService)
        {
            _gameInputSystem =  gameInputSystem;
            _upgradesWindowView = gameCanvasView.UpgradesWindowView;
            _gameStateModel = gameStateModel;
            
            _upgradeSessionModel = upgradeSessionModel;
            _upgradeWindowModel = upgradeWindowModel;
            
            _localizationService = localizationService;
            
            foreach (var config in upgradeConfigs)
                _upgradeConfigs.Add(config.UpgradeType, config);
        }

        public void Initialize()
        {
            _gameInputSystem.OnShowUpgradePanelInputEvent
                .Subscribe(_ => ChangeWindowState())
                .AddTo(_disposables);
            
            _upgradesWindowView.Hide();
            
            _upgradesWindowView.ApplyText.Initialize(_localizationService);
            
            _upgradeSessionModel.AvailableSkillPoints
                .Subscribe(points=>
                {
                    _upgradesWindowView.SkillPointsText.
                        SetText($"{_localizationService.GetText(_upgradesWindowView.SkillPointsText.Key)} {points}");
                })
                .AddTo(_disposables);
            
            _upgradeSessionModel.OnSuccessUpgradeEvent
                .Subscribe(_=>_upgradesWindowView.SetApplyButtonVisibility(true))
                .AddTo(_disposables);
            
            _upgradesWindowView.OnApplyButtonClicked
                .Subscribe(_ => ChangeWindowState())
                .AddTo(_disposables);
            
            SaveExtension.player.Language.OnChangeEvent
                .Subscribe(_ =>
                {
                    foreach (var upgrade in _upgradesWindowView.Upgrades.Values)
                        upgrade.LocalizationText.SetText(_localizationService.GetText(upgrade.LocalizationText.Key));
                    
                    _upgradesWindowView.SkillPointsText.
                        SetText($"{_localizationService.GetText(_upgradesWindowView.SkillPointsText.Key)} " +
                                $"{_upgradeSessionModel.AvailableSkillPoints.Value}");
                })
                .AddTo(_disposables);

            foreach (var skill in _upgradeSessionModel.Skills)
            {
                skill.Value.CurrentLevel
                    .Subscribe(level => RefreshAllUpgradesState())
                    .AddTo(_disposables);
            }
        }

        private void RefreshAllUpgradesState()
        {
            foreach (var skill in _upgradeSessionModel.Skills)
            {
                if (skill.Value.CurrentLevel.Value == skill.Value.MaxLevel.Value)
                    _upgradesWindowView.Upgrades[skill.Key].SetUpgradeLevel("max");
                else
                    _upgradesWindowView.Upgrades[skill.Key].SetUpgradeLevel($"lvl {skill.Value.CurrentLevel.Value}");
            }
        }

        private void ChangeWindowState()
        {
            _upgradeWindowModel.UpgradesOpen = !_upgradeWindowModel.UpgradesOpen;

            if (_upgradeWindowModel.UpgradesOpen)
            {
                _upgradesWindowView.Show();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                _upgradesWindowView.Hide();
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            _upgradesWindowView.SetApplyButtonVisibility(false);
            _gameStateModel.UpgradesIsOpen.Value = _upgradeWindowModel.UpgradesOpen;
            
            RefreshAllUpgradesState();
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}