using System.Collections.Generic;
using Source.Scripts.Configs;
using Source.Scripts.Upgrades.Enums;

namespace Source.Scripts.Upgrades
{
    public sealed class PercentUpgradeModificator : IUpgradeModificator
    {
        private readonly Dictionary<EUpgradeType, UpgradesConfigSO> _upgradeConfigs = new();

        public PercentUpgradeModificator(UpgradesConfigSO[] upgradeConfigs)
        {
            foreach (var config in upgradeConfigs)
                _upgradeConfigs.Add(config.UpgradeType, config);
        }
        
        public float GetValue(EUpgradeType upgradeType, float baseValue, int level)
        {
            if(level == 0)
                return baseValue;
            
            var percent = _upgradeConfigs[upgradeType].UpgradePercents[level - 1];
            
            return baseValue + (baseValue * percent)/100;
        }
    }
}