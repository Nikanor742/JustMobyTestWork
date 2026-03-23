using R3;
using Source.Scripts.Enemies.Models;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public interface IDamageable
    {
        Subject<TakeDamageModel> OnTakeDamageEvent { get; }
        void TakeDamage(float damage, Vector3 hitPosition);
    }
}