using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Player.Views
{
    public sealed class PlayerHealthView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private Image _healthFill;

        public void SetHealth(float currentHealth, float maxHealth)
        {
            if(maxHealth <= 0)
                return;
            
            _healthText.text = $"{currentHealth}/{maxHealth}";
            _healthFill.fillAmount = currentHealth / maxHealth;
        }
    }
}