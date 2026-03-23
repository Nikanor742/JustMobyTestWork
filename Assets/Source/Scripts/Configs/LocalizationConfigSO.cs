using Source.Scripts.Localization;
using UnityEngine;

namespace Source.Scripts.Configs
{
    [CreateAssetMenu(fileName = "LocalizationConfig", menuName = "Configs/LocalizationConfig")]
    public sealed class LocalizationConfigSO : ScriptableObject
    {
        [field: SerializeField] public string[] Langs { get; private set; }
        [field: SerializeField] public LocalizationData[] LocalizationData { get; private set; }
    }
}