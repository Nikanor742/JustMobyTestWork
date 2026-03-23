using R3;

namespace Source.Scripts.Game.Models
{
    public sealed class GameStateModel
    {
        public ReactiveProperty<bool> UpgradesIsOpen = new();
    }
}