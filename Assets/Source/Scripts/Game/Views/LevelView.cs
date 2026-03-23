using UnityEngine;

namespace Source.Scripts.Game.Views
{
    public sealed class LevelView : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform[] enemySpawnPoints;
        public Transform PlayerSpawnPoint => _playerSpawnPoint;
        public Transform[] EnemySpawnPoints => enemySpawnPoints;
    }
}