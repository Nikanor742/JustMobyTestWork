using R3;
using Source.Scripts.Enemies;
using UnityEngine;

namespace Source.Scripts.Interfaces
{
    public interface IDamageable
    {
         Subject<TakeDamageData> OnTakeDamageEvent { get; }
        void TakeDamage(float damage, Vector3 hitPosition);
    }
}