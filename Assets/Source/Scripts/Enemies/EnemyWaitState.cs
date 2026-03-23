using UnityEngine;

namespace Source.Scripts.Enemies
{
    public sealed class EnemyWaitState : IEnemyState
    {
        private EnemyView _enemyView;
        private readonly EnemyModel _enemyModel;
        private readonly EnemyConfigSO _enemyConfig;
        
        public EnemyWaitState(
            EnemyModel enemyModel, 
            EnemyConfigSO enemyConfig)
        {
            _enemyModel = enemyModel;
            _enemyConfig = enemyConfig;
        }

        public void SetView(EnemyView enemyView) => _enemyView = enemyView;
        
        public void Enter()
        {
            if(_enemyView.NavMeshAgent == null)
                return;
            
            _enemyView.NavMeshAgent.isStopped = true;
            _enemyModel.WaitTimeLeft = _enemyConfig.WaitTime;
        }

        public bool Tick()
        {
            _enemyModel.WaitTimeLeft -= Time.deltaTime;
            return _enemyModel.WaitTimeLeft <= 0;
        }

        public void Exit() { }
    }
}