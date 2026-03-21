using R3;

namespace Source.Scripts.Enemies
{
    public interface IEnemyState
    {
        void Enter();
        bool Tick();
        void Exit();
    }
}