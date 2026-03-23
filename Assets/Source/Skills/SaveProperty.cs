using System;
using R3;
using UnityEngine;

namespace Source.Skills
{
    [Serializable]
    public class SaveProperty<T>
    {
        [NonSerialized] public ReactiveProperty<T> OnChangeEvent = new();
        [SerializeField] private T _savedValue;
        
        public void Init()
        {
            OnChangeEvent = new ReactiveProperty<T>(_savedValue);
        }
        
        public T Value
        {
            get => _savedValue;
            set
            {
                _savedValue = value;
                OnChangeEvent?.OnNext(_savedValue);
            }
        }
    }
}