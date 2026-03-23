using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Views
{
    public sealed class UpgradeView : MonoBehaviour
    {
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private TMP_Text _upgradeName;
        [SerializeField] private TMP_Text _upgradeLevel;

        public Subject<UpgradeView> OnUpgradeButtonClicked { get; } = new();

        public void SetUpgradeName(string upgradeName) => _upgradeName.text = upgradeName;
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