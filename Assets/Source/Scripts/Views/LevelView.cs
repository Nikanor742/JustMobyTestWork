using UnityEngine;

namespace Source.Scripts.Views
{
    public sealed class LevelView : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;
        public Transform PlayerSpawnPoint => _playerSpawnPoint;
    }
}