using R3;
using Source.Scripts.Enemies.Models;
using UnityEngine;

namespace Source.Scripts.Enemies.Providers
{
    public class TakeDamageProvider : MonoBehaviour, IDamageable
    {
        public Subject<TakeDamageModel> OnTakeDamageEvent { get; } = new();

        public void TakeDamage(float damage, Vector3 hitPoint)
        {
            OnTakeDamageEvent?.OnNext(new TakeDamageModel() { Damage = damage, HitPoint = hitPoint });
        }
    }
}