using R3;

namespace Source.Scripts.Upgrades
{
    public sealed class SkillSessionModel
    {
        public ReactiveProperty<int> CurrentLevel = new();
        public ReactiveProperty<int> MaxLevel = new();

        public int AddedLevels;
    }
}