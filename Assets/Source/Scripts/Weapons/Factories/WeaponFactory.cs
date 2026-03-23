using Source.Scripts.Configs;
using Source.Scripts.Player.Views;
using Source.Scripts.Upgrades;
using Source.Scripts.Weapons.Enums;
using UnityEngine;

namespace Source.Scripts.Weapons.Factories
{
    public sealed class WeaponFactory : IWeaponFactory
    {
        private readonly PlayerView _playerView;
        private readonly IBulletFactory _bulletFactory;
        private readonly IUpgradeModificator _upgradeModificator;
        private readonly Camera _camera;

        public WeaponFactory(
            PlayerView playerView, 
            IBulletFactory bulletFactory,
            IUpgradeModificator upgradeModificator, 
            Camera camera)
        {
            _playerView = playerView;
            _bulletFactory = bulletFactory;
            _upgradeModificator = upgradeModificator;
            _camera = camera;
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
                EWeaponType.Pistol =>
                    new SimpleWeapon(weaponConfig, weaponView, _bulletFactory, _upgradeModificator, _camera),
                EWeaponType.MachineGun =>
                    new SimpleWeapon(weaponConfig, weaponView, _bulletFactory, _upgradeModificator, _camera),
                EWeaponType.GrenadeGun =>
                    new GrenadeGunWeapon(weaponConfig, weaponView, _bulletFactory, _upgradeModificator, _camera)
            };
            
            
            return weapon;
        }
    }   
}