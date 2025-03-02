using ServiceLocator.Controls;
using ServiceLocator.Player;
using UnityEngine;

namespace ServiceLocator.Vision
{
    public class CameraService
    {
        // Private Variables
        private Camera mainCamera;
        private CameraConfig cameraConfig;

        private float pitch;  // Pitch is vertical angle
        private float yaw;  // Yaw is horizontal angle

        // Private Services
        private InputService inputService;
        private PlayerService playerService;

        public CameraService(Camera _mainCamera, CameraConfig _cameraConfig)
        {
            // Setting Variables
            mainCamera = _mainCamera;
            cameraConfig = _cameraConfig;

            pitch = cameraConfig.initialVerticalAngle;
            yaw = cameraConfig.initialHorizontalAngle;
        }
        public void Init(InputService _inputService, PlayerService _playerService)
        {
            // Setting Services
            inputService = _inputService;
            playerService = _playerService;
        }
        public void Destroy() { }
        public void Update() => RotateCamera();

        void RotateCamera()
        {
            float mouseX = inputService.CameraRotation.x * cameraConfig.cameraSensitivity * Time.deltaTime;
            float mouseY = inputService.CameraRotation.y * cameraConfig.cameraSensitivity * Time.deltaTime;

            // Setting horizontal and vertical movement based on mouse movement(delta)
            yaw += mouseX;
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -cameraConfig.verticalMovementRestricton.x, cameraConfig.verticalMovementRestricton.y);

            // Fetching player position based on height offset
            Vector3 playerPosition =
                playerService.GetPlayerController().GetTransform().position + Vector3.up * cameraConfig.heightOffsetFromPlayer;

            // Calculating rotation from yaw and pitch
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

            // To keep constant distance behind player
            Vector3 cameraOffset = rotation * new Vector3(0f, 0f, -cameraConfig.distanceFromPlayer);

            // Setting camera Position
            mainCamera.transform.position = playerPosition + cameraOffset;
            mainCamera.transform.LookAt(playerPosition);
        }
    }
}