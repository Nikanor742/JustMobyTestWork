using Source.Scripts.Player.Views;
using Source.Scripts.Tutor.Views;
using Source.Scripts.Upgrades.Views;
using UnityEngine;

namespace Source.Scripts.Game.Views
{
    public sealed class GameCanvasView : MonoBehaviour
    {
        [SerializeField] private UpgradesWindowView _upgradesWindowView;
        [SerializeField] private PlayerHealthView _playerHealthView;
        [SerializeField] private TutorView _tutorView;
        
        public UpgradesWindowView UpgradesWindowView => _upgradesWindowView;
        public PlayerHealthView PlayerHealthView => _playerHealthView;
        public TutorView TutorView => _tutorView;
    }
}