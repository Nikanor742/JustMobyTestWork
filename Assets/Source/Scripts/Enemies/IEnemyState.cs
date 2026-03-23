using R3;

namespace Source.Scripts.Enemies
{
    public interface IEnemyState
    {
        void SetView(EnemyView enemyView);
        void Enter();
        bool Tick();
        void Exit();
    }
}