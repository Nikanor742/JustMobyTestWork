using R3;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class TakeDamageProvider : MonoBehaviour, IDamageable
    {
        public Subject<TakeDamageData> OnTakeDamageEvent { get; } = new();

        public void TakeDamage(float damage, Vector3 hitPoint)
        {
            OnTakeDamageEvent?.OnNext(new TakeDamageData() { Damage = damage, HitPoint = hitPoint });
        }
    }
}