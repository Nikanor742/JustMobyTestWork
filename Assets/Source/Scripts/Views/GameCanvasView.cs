using Source.Scripts.Player;
using UnityEngine;

namespace Source.Scripts.Views
{
    public sealed class GameCanvasView : MonoBehaviour
    {
        [SerializeField] private UpgradesWindowView _upgradesWindowView;
        [SerializeField] private PlayerHealthView _playerHealthView;
        
        public UpgradesWindowView UpgradesWindowView => _upgradesWindowView;
        public PlayerHealthView PlayerHealthView => _playerHealthView;
    }
}