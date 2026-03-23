using R3;
using UnityEngine;

namespace Source.Scripts.Models
{
    public sealed class PlayerModel
    {
        public float CurrentHeadRotation;
        public Vector3 Velocity;

        public ReactiveProperty<float> CurrentHealth = new();
        public ReactiveProperty<float> MaxHealth = new();
    }
}