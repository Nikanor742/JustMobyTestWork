using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Scripts.Game.Views;
using Source.Scripts.Localization;
using Source.Scripts.Tutor.Views;

namespace Source.Scripts.Tutor.Systems
{
    public sealed class TutorSystem : IDisposable
    {
        private readonly LocalizationService _localizationService;
        private readonly TutorView _tutorView;
        
        private CancellationTokenSource _cancellationTokenSource;

        public TutorSystem(
            LocalizationService localizationService,
            GameCanvasView gameCanvasView)
        {
            _localizationService = localizationService;
            _tutorView = gameCanvasView.TutorView;
        }

        public void Initialize()
        {
            foreach (var tutorText in _tutorView.TutorTextViews)
            {
                tutorText.Initialize(_localizationService);
            }
            _cancellationTokenSource = new CancellationTokenSource();
            
            HideTutor(_cancellationTokenSource.Token).Forget();
        }

        private async UniTask HideTutor(CancellationToken cancellationToken)
        {
            await UniTask.WaitForSeconds(5, cancellationToken : cancellationToken);
            _tutorView.gameObject.SetActive(false);
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
        }
    }
}