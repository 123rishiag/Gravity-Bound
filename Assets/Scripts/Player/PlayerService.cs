using ServiceLocator.Controls;

namespace ServiceLocator.Player
{
    public class PlayerService
    {
        // Private Variables
        public PlayerConfig playerConfig;
        private PlayerController playerController;

        public PlayerService(PlayerConfig _playerConfig) => playerConfig = _playerConfig;

        public void Init(InputService _inputService)
        {
            playerController = new PlayerController(playerConfig.playerData, playerConfig.playerPrefab,
                _inputService);
        }
        public void Destroy() { }
        public void Update() => playerController.Update();

    }
}