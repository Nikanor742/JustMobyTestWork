using Source.Scripts.Scriptable;
using Source.Scripts.Views;

namespace Source.Scripts.Interfaces
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