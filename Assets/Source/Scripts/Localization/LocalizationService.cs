using System.Collections.Generic;
using Source.Scripts.Configs;
using Source.Scripts.Save;

namespace Source.Scripts.Localization
{
    public sealed class LocalizationService
    {
        private readonly LocalizationConfigSO _localizationConfig;
        
        private readonly Dictionary<string, string[]> _localizations = new ();
        
        public LocalizationService(LocalizationConfigSO localizationConfig)
        {
            _localizationConfig  = localizationConfig;
        }

        public void Initialize()
        {
            foreach (var data in _localizationConfig.LocalizationData)
                _localizations.Add(data.Key, data.Translates);
        }
        
        public string GetText(string key)
        {
            if (_localizations.TryGetValue(key, out var text))
            {
                return text[SaveExtension.player.Language.Value];
            }
            
            return $"Key: {key} not found";
        }
    }
}