using Source.Scripts.Configs;
using Source.Scripts.Enemies.Views;
using UnityEngine;

namespace Source.Scripts.Enemies.Factories
{
    public interface IEnemyFactory
    {
        EnemyView Create(EnemyConfigSO config, Transform spawnPoint);
    }
}