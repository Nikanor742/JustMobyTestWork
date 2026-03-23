using System;
using Source.Data;
using Source.Scripts.Data;
using UnityEngine;

namespace Source.Scripts.Views
{
    public sealed class DataDrawer : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        private void Awake()
        {
            _playerData = SaveExtension.player;
        }
    }
}