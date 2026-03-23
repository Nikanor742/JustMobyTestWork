using Source.Scripts.Upgrades.Enums;
using UnityEngine;

namespace Source.Scripts.Configs
{
    [CreateAssetMenu(fileName = "UpgradesConfig", menuName = "Configs/UpgradesConfig")]
    public sealed class UpgradesConfigSO : ScriptableObject
    {
        [field: SerializeField] public string UpgradeName { get; private set; }
        [field: SerializeField] public EUpgradeType UpgradeType {get; private set;}
        [field: SerializeField] public float[] UpgradePercents {get; private set;}
    }
}