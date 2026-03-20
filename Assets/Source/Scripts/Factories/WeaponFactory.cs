using Source.Scripts.Enums;
using Source.Scripts.Interfaces;
using Source.Scripts.Scriptable;
using Source.Scripts.Views;
using Source.Scripts.Weapons;
using UnityEngine;

namespace Source.Scripts.Factories
{
    public sealed class WeaponFactory
    {
        private PlayerView _playerView;

        public WeaponFactory(PlayerView playerView)
        {
            _playerView = playerView;
        }

        public IWeapon Create(WeaponConfigSO weaponConfig)
        {
            var weaponView = Object.Instantiate(weaponConfig.WeaponView, _playerView.WeaponRoot);
            
            weaponView.transform.localPosition = weaponConfig.LocalPosition;
            weaponView.transform.localEulerAngles = weaponConfig.LocalRotation;
            weaponView.transform.localScale = weaponConfig.LocalScale;
            
            weaponView.gameObject.SetActive(false);
            
            IWeapon weapon = weaponConfig.WeaponType switch
            {
                EWeaponType.SimpleWeapon => new SimpleWeapon(weaponConfig, weaponView),
                _ => new SimpleWeapon(weaponConfig, weaponView)
            };
            
            
            return weapon;
        }
    }   
}