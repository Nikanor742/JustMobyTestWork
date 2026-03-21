using UnityEngine;

namespace Source.Scripts.Views
{
    public sealed class LevelView : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform _enemySpawnPoint;
        public Transform PlayerSpawnPoint => _playerSpawnPoint;
        public Transform EnemySpawnPoint => _enemySpawnPoint;
    }
}