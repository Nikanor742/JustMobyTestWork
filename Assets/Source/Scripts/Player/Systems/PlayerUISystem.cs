using System;
using R3;
using Source.Scripts.Game.Views;
using Source.Scripts.Player.Models;
using Source.Scripts.Player.Views;

namespace Source.Scripts.Player.Systems
{
    public sealed class PlayerUISystem : IDisposable
    {
        private readonly PlayerModel _playerModel;
        private readonly PlayerHealthView  _playerHealthView;
        
        private readonly CompositeDisposable _disposables = new();
        
        public PlayerUISystem(
            PlayerModel playerModel,
            GameCanvasView gameCanvasView)
        {
            _playerModel = playerModel;
            _playerHealthView = gameCanvasView.PlayerHealthView;
        }

        public void Initialize()
        {
            _playerModel.CurrentHealth
                .Subscribe(health =>
                {
                    _playerHealthView.SetHealth(health, _playerModel.MaxHealth.Value);
                })
                .AddTo(_disposables);
            
            _playerModel.MaxHealth
                .Subscribe(maxHealth =>
                {
                    _playerHealthView.SetHealth(_playerModel.CurrentHealth.Value, maxHealth);
                })
                .AddTo(_disposables);
            
            _playerHealthView.SetHealth(_playerModel.CurrentHealth.Value, _playerModel.MaxHealth.Value);
        }


        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}