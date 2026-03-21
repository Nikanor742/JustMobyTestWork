using R3;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public sealed class EnemyModel
    {
        public float WaitTimeLeft;

        public float MaxHp;
        public ReactiveProperty<float> CurrentHp { get; } = new();
        
        public float HealthNormalized => MaxHp <= 0 ? 0 : CurrentHp.Value / MaxHp;

        public bool IsDead;
        
        public IEnemyState CurrentState;

        public void ApplyDamage(float damage)
        {
            if (damage <= 0 || CurrentHp.Value <= 0)
                return;

            CurrentHp.Value = Mathf.Clamp(CurrentHp.Value - damage, 0, MaxHp);
        }
    }
}