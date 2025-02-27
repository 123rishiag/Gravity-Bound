using UnityEngine;

namespace ServiceLocator.Vision
{
    public class CameraService
    {
        // Private Variables
        private Camera mainCamera;

        public CameraService(Camera _camera) => mainCamera = _camera;
        public void Init() { }
        public void Destroy() { }
        public void Update() { }

        // Setters
        public void SetFollow(Transform _transform) => mainCamera.transform.position = _transform.position;
    }
}