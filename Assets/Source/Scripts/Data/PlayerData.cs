using System;
using System.Collections.Generic;
using Source.Scripts.Enums;
using Source.Skills;

namespace Source.Scripts.Data
{
    [Serializable]
    public sealed class PlayerData
    {
        public SaveProperty<int> SkillPoints = new();
        
        public Dictionary<EUpgradeType, SaveProperty<int>> UpgradeStats = new();

        public PlayerData()
        {
            InitProperties();
        }

        public void InitProperties()
        {
            SkillPoints.Init();
            
            foreach(var pair in UpgradeStats.Values)
                pair.Init();
        }
    }
}