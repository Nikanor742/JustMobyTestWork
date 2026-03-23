using UnityEngine;

namespace Source.Scripts.Game.Views
{
    public abstract class GameWindow : MonoBehaviour
    {
        public virtual void Show()
        {
            gameObject.SetActive(true);
        } 
        
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        } 
    }
}