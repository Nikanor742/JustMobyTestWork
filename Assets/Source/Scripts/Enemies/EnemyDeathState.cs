using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class EnemyDeathState : IEnemyState
    {
        private EnemyView _enemyView;
        private readonly EnemyModel _enemyModel;
        private readonly EnemyConfigSO _enemyConfig;
        
        private readonly int _deathTriggerHash = Animator.StringToHash("Death");
        
        public EnemyDeathState(
            EnemyModel enemyModel, 
            EnemyConfigSO enemyConfig)
        {
            _enemyModel = enemyModel;
            _enemyConfig = enemyConfig;
        }
        
        public void SetView(EnemyView enemyView) => _enemyView = enemyView;
        
        public void Enter()
        {
            _enemyView.NavMeshAgent.isStopped = true;
            _enemyView.Animator.SetTrigger(_deathTriggerHash);
        }

        public bool Tick()
        {
            _enemyModel.DeathTime += Time.deltaTime;
            if (_enemyModel.DeathTime >= _enemyConfig.DeathTime)
                return true;
            
            return false;
        }

        public void Exit() { }
    }
}