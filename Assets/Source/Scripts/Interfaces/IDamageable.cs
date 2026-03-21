using R3;

namespace Source.Scripts.Interfaces
{
    public interface IDamageable
    {
        Subject<float> OnTakeDamageEvent { get; }
        void TakeDamage(float damage);
    }
}