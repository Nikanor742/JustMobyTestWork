using UnityEngine;

namespace Source.Scripts.Scriptable
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public sealed class PlayerConfigSO : ScriptableObject
    {
        [Header("Player")]
        [field: SerializeField, Range(1f, 100f)] public float BaseHealth { get; private set; } = 100f; 
        [field: SerializeField, Range(1,200)] public float MaxHeadAngle { get; private set; } = 90f;
        [field: SerializeField, Range(1,200)] public float WalkSpeed { get; private set; } = 5f;
        [field: SerializeField, Range(1,200)] public float RunSpeed { get; private set; } = 10f;
        [field: SerializeField, Range(1,200)] public float JumpHeight { get; private set; } = 2f;
        [field: SerializeField] public float Gravity { get; private set; } = -9.81f;
    }
}