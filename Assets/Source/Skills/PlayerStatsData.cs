using System;
using R3;
using Source.Scripts.Enums;

namespace Source.Skills
{
    [Serializable]
    public class PlayerStatsData
    {
        public int Level;

        public PlayerStatsData(int level)
        {
            Level = level;
        }
    }
}