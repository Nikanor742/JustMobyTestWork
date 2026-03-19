using System;
using R3;
using Unity.VisualScripting;
using UnityEngine;

namespace  Game.Gameplay
{
    public sealed class GameInitializer: IInitializable, IDisposable
    {
        private readonly InputController _inputController;
        
        private readonly CompositeDisposable _disposables = new();
        
        public GameInitializer(InputController inputController)
        {
            _inputController =  inputController;
            Debug.Log("GameInitializer.Initialize()");
        }
        
        public void Initialize()
        {
            Debug.Log("Init!" + _inputController);

            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    Debug.Log(_inputController.MoveInput);
                })
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}
