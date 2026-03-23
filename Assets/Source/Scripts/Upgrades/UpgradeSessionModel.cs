using System.Collections.Generic;
using R3;
using Source.Scripts.Enums;
using Source.Scripts.Upgrades;

namespace Source.Scripts.Models
{
    public sealed class UpgradeSessionModel
    {
        public ReactiveProperty<int> AvailableSkillPoints { get; } = new();
        public Dictionary<EUpgradeType, SkillSessionModel> Skills { get; } = new();

        public int SpentSkillPoints;
        
        public Subject<Unit> OnSuccessUpgradeEvent {get;} = new ();
    }
}