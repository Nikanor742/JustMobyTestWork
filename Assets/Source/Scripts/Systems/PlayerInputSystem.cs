using System;
using R3;
using Source.Scripts.Enums;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.Scripts.Systems
{
    public sealed class PlayerInputSystem : IDisposable
    {
        private GameActions _gameActions;
        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }
        
        public bool SprintInput { get; private set; }
        public bool JumpInput { get; private set; }
        public bool ShootInput { get; private set; }

        public readonly Subject<int> OnWeaponChangeInputEvent = new();
        
        public void Initialize(GameActions gameActions)
        {
            _gameActions = gameActions;
            
            _gameActions.Player.Move.performed += MovePerformed;
            _gameActions.Player.Move.canceled += MoveCanceled;
            
            _gameActions.Player.Look.performed += LookPerformed;
            _gameActions.Player.Look.canceled += LookCanceled;
            
            _gameActions.Player.Sprint.performed += SprintPerformed;
            _gameActions.Player.Sprint.canceled += SprintCanceled;
            
            _gameActions.Player.Jump.performed += JumpPerformed;
            _gameActions.Player.Jump.canceled += JumpCanceled;
            
            _gameActions.Player.Shoot.performed += ShootPerformed;
            _gameActions.Player.Shoot.canceled += ShootCanceled;

            _gameActions.Player.Weapon_1.performed += Weapon1Performed;
            _gameActions.Player.Weapon_2.performed += Weapon2Performed;
            _gameActions.Player.Weapon_3.performed += Weapon3Performed;
        }

        private void MovePerformed(InputAction.CallbackContext obj) => MoveInput = obj.ReadValue<Vector2>();
        private void MoveCanceled(InputAction.CallbackContext obj) => MoveInput = Vector2.zero;
        
        private void LookPerformed(InputAction.CallbackContext obj) => LookInput = obj.ReadValue<Vector2>();
        private void LookCanceled(InputAction.CallbackContext obj) => LookInput = Vector2.zero;
        
        private void SprintPerformed(InputAction.CallbackContext obj) =>  SprintInput = true;
        private void SprintCanceled(InputAction.CallbackContext obj) => SprintInput = false;
        
        private void JumpPerformed(InputAction.CallbackContext obj) =>  JumpInput = true;
        private void JumpCanceled(InputAction.CallbackContext obj) => JumpInput = false;
        
        private void ShootPerformed(InputAction.CallbackContext obj) => ShootInput = true; 
        private void ShootCanceled(InputAction.CallbackContext obj) => ShootInput = false; 
        
        private void Weapon1Performed(InputAction.CallbackContext obj) => OnWeaponChangeInputEvent?.OnNext(0);
        private void Weapon2Performed(InputAction.CallbackContext obj) => OnWeaponChangeInputEvent?.OnNext(1);
        private void Weapon3Performed(InputAction.CallbackContext obj) => OnWeaponChangeInputEvent?.OnNext(2);
        

        public void Dispose()
        {
            _gameActions.Player.Move.performed -= MovePerformed;
            _gameActions.Player.Move.canceled -= MoveCanceled;
            
            _gameActions.Player.Look.performed -= LookPerformed;
            _gameActions.Player.Look.canceled -= LookCanceled;
            
            _gameActions.Player.Sprint.performed -= SprintPerformed;
            _gameActions.Player.Sprint.canceled -= SprintCanceled;
            
            _gameActions.Player.Jump.performed -= JumpPerformed;
            _gameActions.Player.Jump.canceled -= JumpCanceled;
            
            _gameActions.Player.Shoot.performed -= ShootPerformed;
            _gameActions.Player.Shoot.canceled -= ShootCanceled;
            
            _gameActions.Player.Weapon_1.performed -= Weapon1Performed;
            _gameActions.Player.Weapon_2.performed -= Weapon2Performed;
            _gameActions.Player.Weapon_3.performed -= Weapon3Performed;
        }
    }
}