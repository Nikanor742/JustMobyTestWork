using Source.Scripts.Enemies.Providers;
using UnityEngine;
using UnityEngine.AI;

namespace Source.Scripts.Enemies.Views
{
    public sealed class EnemyView : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private TakeDamageProvider _takeDamageProvider;
        [SerializeField] private Animator _animator;
        
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public Renderer Renderer => _renderer;
        public TakeDamageProvider TakeDamageProvider => _takeDamageProvider;
        public Animator Animator => _animator;
        
        private MaterialPropertyBlock _propertyBlock;
        private readonly int _baseColorId = Shader.PropertyToID("_BaseColor");

        private void Awake()
        {
            _propertyBlock = new MaterialPropertyBlock();
        }

        public void SetHealthNormalized(float health)
        {
            var color = Color.Lerp(Color.red, Color.green, health);
            _renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetColor(_baseColorId, color);
            _renderer.SetPropertyBlock(_propertyBlock);
        }
    }
}