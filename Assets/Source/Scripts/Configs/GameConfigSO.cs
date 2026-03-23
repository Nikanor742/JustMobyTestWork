using UnityEngine;

namespace Source.Scripts.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public sealed class GameConfigSO : ScriptableObject
    {
        [field: SerializeField, Range(0, 1)] public float MouseSensitivity { get; private set; } = 1f;
    }
}