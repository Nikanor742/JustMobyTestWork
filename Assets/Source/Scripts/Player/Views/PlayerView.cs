using UnityEngine;

namespace Source.Scripts.Player.Views
{
    public sealed class PlayerView : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _bodyTransform;
        [SerializeField] private Transform _weaponRoot;
        
        public CharacterController CharacterController => _characterController;
        public Transform BodyTransform => _bodyTransform;
        public Transform WeaponRoot => _weaponRoot;
    }
}

