namespace ServiceLocator.Player
{
    public class PlayerService
    {
        // Private Variables
        public PlayerConfig playerConfig;
        private PlayerController playerController;

        public PlayerService(PlayerConfig _playerConfig) => playerConfig = _playerConfig;

        public void Init() => playerController = new PlayerController(playerConfig.playerPrefab);
        public void Destroy() { }
        public void Update() { }

    }
}