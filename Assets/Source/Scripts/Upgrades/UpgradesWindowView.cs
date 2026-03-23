using System.Collections.Generic;
using R3;
using Source.Scripts.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Views
{
    public sealed class UpgradesWindowView : GameWindow
    {
        [SerializeField] private TMP_Text _skillPointsText;
        [SerializeField] private Button _applyButton;
        [SerializeField] private UpgradeView _upgradeTemplate;
        [SerializeField] private Dictionary<EUpgradeType, UpgradeView> _upgrades = new();
        
        public UpgradeView UpgradeTemplate => _upgradeTemplate;
        public Dictionary<EUpgradeType, UpgradeView> Upgrades => _upgrades;
        
        public readonly Subject<Unit> OnApplyButtonClicked = new ();

        private void Awake()
        {
            _applyButton.onClick.AddListener(()=> OnApplyButtonClicked?.OnNext(Unit.Default));
        }

        public void SetApplyButtonVisibility(bool visible) => _applyButton.gameObject.SetActive(visible);
        
        public void SetSkillPointsText(int points)
        {
            _skillPointsText.text = $"skill points: {points}";
        }

        private void OnDestroy()
        {
            _applyButton.onClick.RemoveAllListeners();
        }
    }
}