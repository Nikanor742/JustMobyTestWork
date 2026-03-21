using Lean.Pool;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public sealed class EnemyFactory : IEnemyFactory
    {
        public EnemyView Create(EnemyConfigSO config, Transform spawnPoint)
        {
            var enemy = LeanPool.Spawn(config.EnemyPrefab);
            
            enemy.transform.position = spawnPoint.position;
            enemy.transform.rotation = spawnPoint.rotation;
            enemy.NavMeshAgent.speed = config.EnemyMoveSpeed;
            enemy.NavMeshAgent.angularSpeed = config.EnemyAngularSpeed;
            
            return enemy;
        }
    }
}