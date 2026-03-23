using UnityEngine;

namespace Source.Scripts.Weapons.Views
{
    public sealed class WeaponView : MonoBehaviour
    {
        public Transform BulletStartPoint => _bulletStartPoint;
        public ParticleSystem ShootFX => _shootFX;
        
        [SerializeField] private Transform _bulletStartPoint;
        [SerializeField] private ParticleSystem _shootFX;
    }
}