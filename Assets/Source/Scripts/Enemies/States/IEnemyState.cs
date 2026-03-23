using Source.Scripts.Enemies.Views;

namespace Source.Scripts.Enemies.States
{
    public interface IEnemyState
    {
        void SetView(EnemyView enemyView);
        void Enter();
        bool Tick();
        void Exit();
    }
}