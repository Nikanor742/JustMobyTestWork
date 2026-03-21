using Lean.Pool;
using Source.Scripts.Interfaces;
using Source.Scripts.Scriptable;
using Source.Scripts.Views;

namespace Source.Scripts.Factories
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