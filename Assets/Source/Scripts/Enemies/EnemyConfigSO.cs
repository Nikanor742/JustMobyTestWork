using UnityEngine;

namespace Source.Scripts.Enemies
{
    
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig")]
    public sealed class EnemyConfigSO : ScriptableObject
    {
        [field: SerializeField] public EnemyView EnemyPrefab { get; private set; }
        [field: SerializeField, Range(1,50)] public int MaxEnemyCount { get; private set; } = 10;
        [field: SerializeField, Range(1,10)] public float EnemyMoveSpeed { get; private set; } = 7f;
        [field: SerializeField, Range(50,200)] public float EnemyAngularSpeed { get; private set; } = 120f;
        [field: SerializeField, Range(1,100)] public float RandomMovePointBound { get; private set; } = 5;
        [field: SerializeField, Range(1,10)] public float WaitTime { get; private set; } = 5;
        [field: SerializeField, Range(1,10)] public float SpawnDelay { get; private set; } = 5;
        [field: SerializeField, Range(1,1000)] public float MaxHp { get; private set; } = 100;
    }
}