using DG.Tweening;
using Lean.Pool;
using Source.Data;
using Source.Scripts.Enums;
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
        private readonly IBulletFactory _bulletFactory;
        private readonly IUpgradeModificator _upgradeModificator;

        private float _nextShootTime;
        
        private Camera _camera;
        
        public SimpleWeapon(
            WeaponConfigSO weaponConfig, 
            WeaponView weaponView, 
            IBulletFactory bulletFactory,
            IUpgradeModificator upgradeModificator,
            Camera camera)
        {
            _weaponConfig = weaponConfig;
            _weaponView = weaponView;
            _bulletFactory = bulletFactory;
            _upgradeModificator = upgradeModificator;
            _camera = camera;
        }

        public WeaponConfigSO WeaponConfig => _weaponConfig;
        public WeaponView WeaponView => _weaponView;
        public IUpgradeModificator UpgradeModificator => _upgradeModificator;

        public void Shoot()
        {
            if (Time.time < _nextShootTime)
                return;
            
            _nextShootTime = Time.time + _weaponConfig.FireRate;
            _weaponView.ShootFX.Play();

            var bullet = _bulletFactory.Create(_weaponConfig);
            bullet.transform.position = _weaponView.BulletStartPoint.position;
            bullet.transform.rotation = _weaponView.BulletStartPoint.rotation;
            bullet.gameObject.SetActive(true);
            
            var screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
            var ray = _camera.ScreenPointToRay(screenCenter);
            
            Vector3 endPoint;
            if (Physics.Raycast(ray, out var hit, 200, _weaponConfig.HitMask))
            {
                endPoint = hit.point;
                if (hit.collider.TryGetComponent(out IDamageable damageable))
                {
                    var damage = _upgradeModificator.GetValue(EUpgradeType.Damage, _weaponConfig.Damage,
                        SaveExtension.player.UpgradeStats[EUpgradeType.Damage].Level);
                    
                    damageable.TakeDamage(damage, hit.point);
                }
            }
            else
            {
                endPoint = ray.origin + ray.direction * 200;
            }
            
            var duration = Vector3.Distance(bullet.transform.position, endPoint) / _weaponConfig.BulletSpeed;
            
            bullet.transform.DOMove(endPoint, duration)
                .SetEase(Ease.Linear)
                .OnComplete(() => LeanPool.Despawn(bullet));
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