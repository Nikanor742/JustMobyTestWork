using Source.Scripts.Enums;

namespace Source.Scripts.Interfaces
{
    public interface IUpgradeModificator
    {
        float GetValue(EUpgradeType upgradeType, float baseValue, int level);
    }
}