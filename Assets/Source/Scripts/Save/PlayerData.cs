using System;
using System.Collections.Generic;
using Source.Scripts.Upgrades.Enums;

namespace Source.Scripts.Save
{
    [Serializable]
    public sealed class PlayerData
    {
        public SaveProperty<int> Language = new();
        public SaveProperty<int> SkillPoints = new();
        
        public Dictionary<EUpgradeType, SaveProperty<int>> UpgradeStats = new();

        public PlayerData()
        {
            InitProperties();
            Language.Value = 1;
        }

        public void InitProperties()
        {
            SkillPoints.Init();
            Language.Init();
            
            foreach(var pair in UpgradeStats.Values)
                pair.Init();
        }
    }
}