using UnityEngine;

namespace Source.Scripts.Views
{
    public sealed class WeaponView : MonoBehaviour
    {
        public Transform BulletStartPoint => _bulletStartPoint;
        public ParticleSystem ShootFX => _shootFX;
        
        [SerializeField] private Transform _bulletStartPoint;
        [SerializeField] private ParticleSystem _shootFX;
    }
}