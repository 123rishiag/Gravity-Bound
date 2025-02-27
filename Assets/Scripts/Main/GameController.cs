using ServiceLocator.Controls;
using ServiceLocator.Vision;

namespace ServiceLocator.Main
{
    public class GameController
    {
        // Private Services
        private GameService gameService;
        private InputService inputService;
        private CameraService cameraService;

        public GameController(GameService _gameService)
        {
            gameService = _gameService;
            CreateServices();
            InjectDependencies();
        }

        private void CreateServices()
        {
            inputService = new InputService();
            cameraService = new CameraService(gameService.mainCamera);
        }
        private void InjectDependencies()
        {
            inputService.Init();
            cameraService.Init();
        }

        public void Destroy()
        {
            inputService.Destroy();
            cameraService.Destroy();
        }

        public void Update()
        {
            inputService.Update();
        }
    }
}