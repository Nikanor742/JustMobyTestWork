using System;
using R3;
using UnityEngine;

namespace Source.Scripts.Save
{
    [Serializable]
    public class SaveProperty<T>
    {
        [NonSerialized] public ReactiveProperty<T> OnChangeEvent = new();
        [SerializeField] private T _savedValue;

        public SaveProperty()
        {
            Init();
        }
        
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