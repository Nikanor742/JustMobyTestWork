using Source.Scripts.Views;

namespace Source.Scripts.Systems
{
    public sealed class PlayerSpawnSystem
    {
        private readonly PlayerView _playerView;
        private readonly LevelView _levelView;
        
        public PlayerSpawnSystem(PlayerView playerView, LevelView levelView)
        {
            _playerView = playerView;
            _levelView = levelView;
        }

        public void Initialize()
        {
            _playerView.transform.position = _levelView.PlayerSpawnPoint.position;
            _playerView.transform.rotation = _levelView.PlayerSpawnPoint.rotation;
        }
    }
}