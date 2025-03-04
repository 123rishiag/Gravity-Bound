using ServiceLocator.Controls;
using ServiceLocator.Player;
using ServiceLocator.Vision;

namespace ServiceLocator.Main
{
    public class GameController
    {
        // Private Services
        private GameService gameService;
        private InputService inputService;
        private CameraService cameraService;
        private PlayerService playerService;

        public GameController(GameService _gameService)
        {
            // Setting Services
            gameService = _gameService;
            CreateServices();
            InjectDependencies();
        }

        private void CreateServices()
        {
            inputService = new InputService();
            cameraService = new CameraService(gameService.mainCamera, gameService.cameraConfig);
            playerService = new PlayerService(gameService.playerConfig);
        }
        private void InjectDependencies()
        {
            inputService.Init();
            cameraService.Init(inputService, playerService);
            playerService.Init(inputService);
        }

        public void Destroy()
        {
            inputService.Destroy();
            cameraService.Destroy();
            playerService.Destroy();
        }

        public void Update()
        {
            inputService.Update();
            cameraService.Update();
            playerService.Update();
        }
    }
}