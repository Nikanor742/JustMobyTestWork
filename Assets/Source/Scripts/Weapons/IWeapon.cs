using Source.Scripts.Configs;
using Source.Scripts.Upgrades;
using Source.Scripts.Weapons.Views;

namespace Source.Scripts.Weapons
{
    public interface IWeapon
    {
        WeaponConfigSO WeaponConfig { get; }
        WeaponView WeaponView { get; }
        IUpgradeModificator UpgradeModificator { get; }
        
        void Shoot();

        void Select();
        
        void Deselect();
    }
}