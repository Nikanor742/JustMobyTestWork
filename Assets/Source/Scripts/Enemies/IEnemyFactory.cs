using UnityEngine;

namespace Source.Scripts.Enemies
{
    public interface IEnemyFactory
    {
        EnemyView Create(EnemyConfigSO config, Transform spawnPoint);
    }
}