using System;
using UnityEngine;

namespace ServiceLocator.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptable Objects/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        public PlayerView playerPrefab;
        public PlayerData playerData;
    }

    [Serializable]
    public class PlayerData
    {
        public float maxWalkSpeed = 2f;
        public float maxRunSpeed = 5f;
        public float rotationSpeed = 2f;
        [Range(0, 1)]
        public float sideMovementMultiplier = 0.7f;
        public float gravityScale = 9.81f;
    }
}