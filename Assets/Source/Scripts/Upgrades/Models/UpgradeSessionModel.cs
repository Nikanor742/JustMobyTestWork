using System.Collections.Generic;
using R3;
using Source.Scripts.Upgrades.Enums;

namespace Source.Scripts.Upgrades.Models
{
    public sealed class UpgradeSessionModel
    {
        public ReactiveProperty<int> AvailableSkillPoints { get; } = new();
        public Dictionary<EUpgradeType, SkillSessionModel> Skills { get; } = new();

        public int SpentSkillPoints;
        
        public Subject<Unit> OnSuccessUpgradeEvent {get;} = new ();
    }
}