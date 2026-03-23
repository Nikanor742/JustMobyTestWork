using Source.Scripts.Localization;
using UnityEngine;

namespace Source.Scripts.Tutor.Views
{
    public sealed class TutorView : MonoBehaviour
    {
        [SerializeField] private LocalizationTextView[] _tutorTextViews;
        
        public LocalizationTextView[] TutorTextViews => _tutorTextViews;
    }
}