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
        
        public Dictionary<EUpgradeType, PlayerStatsData> UpgradeStats = new();

        public PlayerData()
        {
            InitFields();
        }

        public void InitFields()
        {
            SkillPoints.Init();
        }
    }
}