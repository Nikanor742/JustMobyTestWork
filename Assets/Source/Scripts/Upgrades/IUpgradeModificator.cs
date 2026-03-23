using Source.Scripts.Upgrades.Enums;

namespace Source.Scripts.Upgrades
{
    public interface IUpgradeModificator
    {
        float GetValue(EUpgradeType upgradeType, float baseValue, int level);
    }
}