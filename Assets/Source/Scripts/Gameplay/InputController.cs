using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Gameplay
{
    public sealed class InputController : IInitializable, IDisposable
    {
        private readonly GameActions _gameActions = new();

        private Vector2 _moveInput;

        public Vector2 MoveInput => _moveInput;

        public void Initialize()
        {
            _gameActions.Player.Move.performed += MovePerformed;
            _gameActions.Player.Move.canceled += MoveCanceled;
        }

        private void MovePerformed(InputAction.CallbackContext obj) => _moveInput = obj.ReadValue<Vector2>();
        private void MoveCanceled(InputAction.CallbackContext obj) => _moveInput = Vector2.zero;
        

        public void Dispose()
        {
            _gameActions.Player.Move.performed -= MovePerformed;
            _gameActions.Player.Move.canceled -= MoveCanceled;
        }
    }
}