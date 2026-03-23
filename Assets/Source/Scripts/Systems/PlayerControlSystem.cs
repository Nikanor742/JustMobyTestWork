using System;
using R3;
using Source.Data;
using Source.Scripts.Enums;
using Source.Scripts.Interfaces;
using Source.Scripts.Models;
using Source.Scripts.Views;
using Source.Scripts.Scriptable;
using UnityEngine;
using Observable = R3.Observable;

namespace Source.Scripts.Systems
{
    public sealed class PlayerControlSystem : IDisposable
    {
        private readonly PlayerView _playerView;
        private readonly PlayerModel _playerModel;
        private readonly PlayerConfigSO _playerConfig;
        private readonly GameConfigSO _gameConfig;
        private readonly PlayerInputSystem _playerInputSystem;
        private readonly GameStateModel _gameStateModel;
        private readonly IUpgradeModificator _upgradeModificator;
        
        private readonly CompositeDisposable _disposables = new();
        
        public PlayerControlSystem(PlayerView playerView,
            PlayerModel playerModel,
            PlayerConfigSO playerConfig,
            GameConfigSO  gameConfig,
            PlayerInputSystem playerInputSystem,
            GameStateModel gameStateModel,
            IUpgradeModificator upgradeModificator)
        {
            _playerView = playerView;
            _playerModel = playerModel;
            _playerConfig = playerConfig;
            _gameConfig = gameConfig;
            _playerInputSystem = playerInputSystem;
            _gameStateModel = gameStateModel;
            _upgradeModificator = upgradeModificator;
        }

        public void Initialize()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            
            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    if (!_gameStateModel.UpgradesIsOpen.Value)
                    {
                        Jump();
                        Look();
                        Move();
                    }
                })
                .AddTo(_disposables);
        }
        
        private void Jump()
        {
            if (_playerInputSystem.JumpInput && _playerView.CharacterController.isGrounded)
                _playerModel.Velocity.y = Mathf.Sqrt(_playerConfig.JumpHeight * -2f * _playerConfig.Gravity);
        }

        private void Look()
        {
            var mouseX = _playerInputSystem.LookInput.x * _gameConfig.MouseSensitivity;
            var mouseY = _playerInputSystem.LookInput.y * _gameConfig.MouseSensitivity;
            
            _playerModel.CurrentHeadRotation -= mouseY;
            _playerModel.CurrentHeadRotation = Mathf.Clamp
                (_playerModel.CurrentHeadRotation, -_playerConfig.MaxHeadAngle, _playerConfig.MaxHeadAngle);
            
            _playerView.transform.Rotate(Vector3.up * mouseX);
            _playerView.BodyTransform.localRotation = Quaternion.Euler(_playerModel.CurrentHeadRotation, 0f, 0f);
        }

        private void Move()
        {
            if (_playerView.CharacterController.isGrounded && _playerModel.Velocity.y < 0)
                _playerModel.Velocity.y = -2f;

            var moveX = _playerInputSystem.MoveInput.x;
            var moveZ = _playerInputSystem.MoveInput.y;

            var move = _playerView.transform.right * moveX + _playerView.transform.forward * moveZ;
            
            var runSpeed = _upgradeModificator.GetValue(EUpgradeType.Speed,
                _playerConfig.RunSpeed,
                SaveExtension.player.UpgradeStats[EUpgradeType.Speed].Level);
            
            var walkSpeed = _upgradeModificator.GetValue(EUpgradeType.Speed,
                _playerConfig.WalkSpeed,
                SaveExtension.player.UpgradeStats[EUpgradeType.Speed].Level);
            
            var speed = _playerInputSystem.SprintInput && _playerView.CharacterController.isGrounded ? 
                runSpeed : walkSpeed;

            _playerModel.Velocity.y += _playerConfig.Gravity * Time.deltaTime;
            
            var motion = move * speed;
            motion.y = _playerModel.Velocity.y;
            
            _playerView.CharacterController.Move(motion * Time.deltaTime);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}