using R3;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class TakeDamageProvider : MonoBehaviour, IDamageable
    {
        public Subject<float> OnTakeDamageEvent { get; } = new();

        public void TakeDamage(float damage)
        {
            OnTakeDamageEvent?.OnNext(damage);
        }
    }
}