using System;
using System.Collections.Generic;
using R3;
using Source.Data;
using Source.Scripts.Enums;
using Source.Scripts.Models;
using Source.Scripts.Scriptable;
using Source.Scripts.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Scripts.Systems
{
    public sealed class UpgradesUISystem : IDisposable
    {
        private readonly GameInputSystem _gameInputSystem;
        private readonly UpgradesWindowView _upgradesWindowView;
        private readonly UpgradeWindowModel _upgradeWindowModel;
        private readonly UpgradeSessionModel _upgradeSessionModel;
        private readonly GameStateModel _gameStateModel;
        
        private readonly Dictionary<EUpgradeType, UpgradesConfigSO> _upgradeConfigs = new();
        
        private readonly CompositeDisposable _disposables = new ();
        
        public UpgradesUISystem(
            GameInputSystem gameInputSystem,
            GameCanvasView  gameCanvasView, 
            GameStateModel gameStateModel,
            UpgradeSessionModel upgradeSessionModel,
            UpgradeWindowModel upgradeWindowModel,
            UpgradesConfigSO[] upgradeConfigs)
        {
            _gameInputSystem =  gameInputSystem;
            _upgradesWindowView = gameCanvasView.UpgradesWindowView;
            _gameStateModel = gameStateModel;
            
            _upgradeSessionModel = upgradeSessionModel;
            _upgradeWindowModel = upgradeWindowModel;
            
            foreach (var config in upgradeConfigs)
                _upgradeConfigs.Add(config.UpgradeType, config);
        }

        public void Initialize()
        {
            _gameInputSystem.OnShowUpgradePanelInputEvent
                .Subscribe(_ => ChangeWindowState())
                .AddTo(_disposables);
            
            _upgradesWindowView.Hide();
            
            _upgradeSessionModel.AvailableSkillPoints
                .Subscribe(points=> _upgradesWindowView.SetSkillPointsText(points))
                .AddTo(_disposables);
            
            _upgradeSessionModel.OnSuccessUpgradeEvent
                .Subscribe(_=>_upgradesWindowView.SetApplyButtonVisibility(true))
                .AddTo(_disposables);
            
            _upgradesWindowView.OnApplyButtonClicked
                .Subscribe(_ => ChangeWindowState())
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