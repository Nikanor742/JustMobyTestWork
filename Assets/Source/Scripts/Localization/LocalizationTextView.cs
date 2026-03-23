using R3;
using Source.Scripts.Save;
using TMPro;
using UnityEngine;

namespace Source.Scripts.Localization
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizationTextView : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField] private TMP_Text _text;

        private LocalizationService _localizationService;

        public string Key => _key;

        public void SetText(string text) => _text.text = text;


        public void Initialize(LocalizationService localizationService, string key = "")
        {
            _localizationService = localizationService;

            if (!string.IsNullOrEmpty(key))
                _key = key;

            SaveExtension.player.Language.OnChangeEvent
                .Subscribe(_ => { SetText(_localizationService.GetText(_key)); })
                .AddTo(this);

            SetText(_localizationService.GetText(_key));
        }
    }
}
