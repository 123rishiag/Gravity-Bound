using ServiceLocator.Player;
using ServiceLocator.Vision;
using UnityEngine;

namespace ServiceLocator.Main
{
    public class GameService : MonoBehaviour
    {
        [Header("Camera Variables")]
        [SerializeField] public Camera mainCamera;
        [SerializeField] public CameraConfig cameraConfig;

        [Header("Game Components")]
        [SerializeField] public PlayerConfig playerConfig;

        // Private Variables
        private GameController gameController;

        private void Start() => gameController = new GameController(this);
        private void OnDestroy() => gameController.Destroy();
        private void Update() => gameController.Update();
    }
}