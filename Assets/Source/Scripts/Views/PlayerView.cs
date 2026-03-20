using Unity.Cinemachine;
using UnityEngine;

namespace Source.Scripts.Views
{
    public sealed class PlayerView : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private CinemachineVirtualCameraBase _camera;
        [SerializeField] private Transform _bodyTransform;
        [SerializeField] private Transform _weaponRoot;
        
        public CharacterController CharacterController => _characterController;
        public CinemachineVirtualCameraBase Camera => _camera;
        public Transform BodyTransform => _bodyTransform;
        public Transform WeaponRoot => _weaponRoot;
    }
}

