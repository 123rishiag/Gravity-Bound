using UnityEngine;

namespace ServiceLocator.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptable Objects/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        public PlayerView playerPrefab;
    }
}