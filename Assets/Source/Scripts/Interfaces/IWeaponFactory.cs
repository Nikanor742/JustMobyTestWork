using Source.Scripts.Scriptable;

namespace Source.Scripts.Interfaces
{
    public interface IWeaponFactory
    {
        IWeapon Create(WeaponConfigSO config);
    }
}