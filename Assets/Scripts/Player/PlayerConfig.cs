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
        public float backwardMovementMultiplier = 0.7f;
        public float sideMovementMultiplier = 0.7f;
        public float movementTransitionFactor = 0.5f;
        public float gravityScale = 9.81f;
    }
}