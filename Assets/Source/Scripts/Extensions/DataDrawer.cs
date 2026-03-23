using Source.Scripts.Save;
using UnityEngine;

namespace Source.Scripts.Extensions
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