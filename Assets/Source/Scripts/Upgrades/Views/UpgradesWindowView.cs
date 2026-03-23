using System.Collections.Generic;
using R3;
using Source.Scripts.Game.Views;
using Source.Scripts.Localization;
using Source.Scripts.Upgrades.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Upgrades.Views
{
    public sealed class UpgradesWindowView : GameWindow
    {
        [SerializeField] private LocalizationTextView _skillPointsText;
        [SerializeField] private LocalizationTextView _applyText;
        [SerializeField] private Button _applyButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private UpgradeView _upgradeTemplate;
        [SerializeField] private Dictionary<EUpgradeType, UpgradeView> _upgrades = new();

        public LocalizationTextView SkillPointsText => _skillPointsText;
        public LocalizationTextView ApplyText => _applyText;
        public UpgradeView UpgradeTemplate => _upgradeTemplate;
        public Dictionary<EUpgradeType, UpgradeView> Upgrades => _upgrades;
        
        public readonly Subject<Unit> OnApplyButtonClicked = new ();
        public readonly Subject<Unit> OnExitButtonClicked = new ();

        private void Awake()
        {
            _applyButton.onClick.AddListener(() => OnApplyButtonClicked?.OnNext(Unit.Default));
            _exitButton.onClick.AddListener(() => OnExitButtonClicked?.OnNext(Unit.Default));
        }

        public void SetApplyButtonVisibility(bool visible) => _applyButton.gameObject.SetActive(visible);

        private void OnDestroy()
        {
            _applyButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }
    }
}