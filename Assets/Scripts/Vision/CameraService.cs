using ServiceLocator.Player;
using UnityEngine;

namespace ServiceLocator.Vision
{
    public class CameraService
    {
        // Private Variables
        private Camera mainCamera;
        private Vector3 positionOffset;

        // Private Services
        private PlayerService playerService;

        public CameraService(Camera _camera, Vector3 _positionOffset)
        {
            mainCamera = _camera;
            positionOffset = _positionOffset;
        }
        public void Init(PlayerService _playerService) => playerService = _playerService;
        public void Destroy() { }
        public void Update() => FollowTransform();

        // Setters
        private void FollowTransform() =>
            mainCamera.transform.position = playerService.GetPlayerController().GetTransform().position + positionOffset;

    }
}