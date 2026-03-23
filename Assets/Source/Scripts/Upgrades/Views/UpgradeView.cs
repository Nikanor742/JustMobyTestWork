using R3;
using Source.Scripts.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Upgrades.Views
{
    public sealed class UpgradeView : MonoBehaviour
    {
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private TMP_Text _upgradeLevel;
        [SerializeField] private LocalizationTextView _localizationText;
        
        public LocalizationTextView LocalizationText => _localizationText;

        public Subject<UpgradeView> OnUpgradeButtonClicked { get; } = new();

        public void SetUpgradeName(string upgradeName) => _localizationText.SetText(upgradeName);
        public void SetUpgradeLevel(string levelText) => _upgradeLevel.text = levelText;

        private void Awake()
        {
            _upgradeButton.onClick.AddListener(() => OnUpgradeButtonClicked?.OnNext(this));
        }

        private void OnDestroy()
        {
            _upgradeButton.onClick.RemoveAllListeners();
        }
    }
}