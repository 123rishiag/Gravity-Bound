using UnityEngine;

namespace ServiceLocator.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayerMask;
        // Private Variables
        private PlayerController playerController;
        private CharacterController playerCharacterController;
        private Animator playerAnimator;

        public void SetController(PlayerController _playerController)
        {
            // Setting Variables
            playerController = _playerController;
            playerCharacterController = GetComponent<CharacterController>();
            playerAnimator = GetComponent<Animator>();
        }

        // Getters
        public CharacterController GetCharacterController() => playerCharacterController;
        public Animator GetAnimator() => playerAnimator;
        public LayerMask GetGroundLayerMask() => groundLayerMask;
    }
}