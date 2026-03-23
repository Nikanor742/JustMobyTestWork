

using R3;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public sealed class EnemyMoveState : IEnemyState
    {
        private EnemyView _enemyView;
        private readonly EnemyModel _enemyModel;
        private readonly EnemyConfigSO _enemyConfig;

        public EnemyMoveState(
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
            
            _enemyView.NavMeshAgent.isStopped = false;
            var currentPos = _enemyView.transform.position;
            var randomPointBound = _enemyConfig.RandomMovePointBound;
            var movePoint = new Vector3(
                Random.Range(currentPos.x - randomPointBound, currentPos.x + randomPointBound),
                0,
                Random.Range(currentPos.z - randomPointBound, currentPos.z + randomPointBound));
            
            _enemyView.NavMeshAgent.SetDestination(movePoint);
        }

        public bool Tick()
        {
            if (_enemyView.NavMeshAgent == null)
                return true;
            
            if (_enemyView.NavMeshAgent.pathPending)
                return false;
            
            if (_enemyView.NavMeshAgent.remainingDistance > _enemyView.NavMeshAgent.stoppingDistance)
                return false;
            
            return true;
        }

        public void Exit() { }
    }
}