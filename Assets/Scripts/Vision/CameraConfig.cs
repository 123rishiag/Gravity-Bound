using UnityEngine;

namespace ServiceLocator.Vision
{
    [CreateAssetMenu(fileName = "CameraConfig", menuName = "Scriptable Objects/CameraConfig")]
    public class CameraConfig : ScriptableObject
    {
        [Header("Camera Rotation")]
        public float cameraSensitivity = 10f; // Sensitivity of Camera Rotation
        public float initialVerticalAngle = 20f; // Camera Vertical Angle at the start of game
        public float initialHorizontalAngle = 0f; // Camera Horizontal Angle at the start of game
        public Vector2 verticalMovementRestricton = new Vector2(20f, 60f); // Clip Vertical Camera Rotation

        [Header("Camera Position")]
        public float distanceFromPlayer = 2f;   // Fixed distance from player
        public float heightOffsetFromPlayer = 1f; // How high above from player to look at
    }
}