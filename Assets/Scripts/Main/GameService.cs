using ServiceLocator.Player;
using UnityEngine;

namespace ServiceLocator.Main
{
    public class GameService : MonoBehaviour
    {
        [Header("Camera Variables")]
        [SerializeField] public Camera mainCamera;

        [Header("Game Components")]
        [SerializeField] public PlayerConfig playerConfig;

        // Private Variables
        private GameController gameController;

        private void Start() => gameController = new GameController(this);
        private void OnDestroy() => gameController.Destroy();
        private void Update() => gameController.Update();
    }
}