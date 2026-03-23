using Source.Scripts.Configs;

namespace Source.Scripts.Weapons.Factories
{
    public interface IWeaponFactory
    {
        IWeapon Create(WeaponConfigSO config);
    }
}