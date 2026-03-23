using Source.Scripts.Configs;
using Source.Scripts.Weapons.Views;

namespace Source.Scripts.Weapons.Factories
{
    public interface IBulletFactory
    {
        BulletView Create(WeaponConfigSO weaponConfig);
    }
}