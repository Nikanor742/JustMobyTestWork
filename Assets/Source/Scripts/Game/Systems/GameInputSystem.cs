using System;
using R3;
using UnityEngine.InputSystem;

namespace Source.Scripts.Game.Systems
{
    public sealed class GameInputSystem : IDisposable
    {
        private GameActions _gameActions;
        
        public readonly Subject<Unit> OnShowUpgradePanelInputEvent = new();
        
        public void Initialize(GameActions gameActions)
        {
            _gameActions = gameActions;
            
            _gameActions.UI.ShowUpgrades.performed += ShowSkillPanelPerformed;
        }

        private void ShowSkillPanelPerformed(InputAction.CallbackContext obj) => 
            OnShowUpgradePanelInputEvent?.OnNext(Unit.Default);

        
        public void Dispose()
        {
            _gameActions.UI.ShowUpgrades.performed -= ShowSkillPanelPerformed;
        }
    }
}