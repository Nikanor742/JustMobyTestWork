using Source.Scripts.Views;
using UnityEngine;

namespace Source.Scripts.Systems
{
    public sealed class LevelSpawnSystem
    {
        private readonly LevelView _levelView;
        
        public LevelSpawnSystem(LevelView levelView)
        {
            _levelView = levelView;
        }

        public void Initialize()
        {
            _levelView.transform.position = Vector3.zero;
        }
    }
}