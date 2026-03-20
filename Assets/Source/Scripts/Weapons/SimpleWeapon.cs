using Source.Scripts.Interfaces;
using Source.Scripts.Scriptable;
using Source.Scripts.Views;
using UnityEngine;

namespace Source.Scripts.Weapons
{
    public sealed class SimpleWeapon : IWeapon
    {
        private readonly WeaponConfigSO _weaponConfig;
        private readonly WeaponView _weaponView;

        private float _nextShootTime;
        
        public SimpleWeapon(WeaponConfigSO weaponConfig ,WeaponView weaponWeaponWeaponWeaponView)
        {
            _weaponConfig = weaponConfig;
            _weaponView = weaponWeaponWeaponWeaponView;
        }

        public WeaponConfigSO WeaponConfig => _weaponConfig;
        public WeaponView WeaponView => _weaponView;

        public void Shoot()
        {
            if (Time.time < _nextShootTime)
                return;
            
            _nextShootTime = Time.time + _weaponConfig.FireRate;
            _weaponView.ShootFX.Play();
        }

        public void Select()
        {
            _weaponView.gameObject.SetActive(true);
        }

        public void Deselect()
        {
            _weaponView.gameObject.SetActive(false);
        }
    }
}