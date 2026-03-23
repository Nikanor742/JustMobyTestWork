using R3;

namespace Source.Scripts.Models
{
    public sealed class GameStateModel
    {
        public ReactiveProperty<bool> UpgradesIsOpen = new();
    }
}