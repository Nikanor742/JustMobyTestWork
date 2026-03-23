using Lean.Pool;
using Source.Scripts.Configs;
using Source.Scripts.Weapons.Views;

namespace Source.Scripts.Weapons.Factories
{
    public sealed class BulletFactory : IBulletFactory
    {
        public BulletView Create(WeaponConfigSO weaponConfig)
        {
            var bullet = LeanPool.Spawn(weaponConfig.BulletView);
            bullet.gameObject.SetActive(false);
            return bullet;
        }
    }
}