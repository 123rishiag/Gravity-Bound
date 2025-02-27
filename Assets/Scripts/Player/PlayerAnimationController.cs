using UnityEngine;

namespace ServiceLocator.Player
{
    public class PlayerAnimationController
    {
        // Private Variables
        private PlayerController playerController;
        private Animator playerAnimator;

        // Animation Hashes
        private readonly int tPoseHash = Animator.StringToHash("TPose");
        private readonly int walkHash = Animator.StringToHash("Walk");

        // Parameter Hashes
        private readonly int movementXHash = Animator.StringToHash("movementX");
        private readonly int movementZHash = Animator.StringToHash("movementZ");
        private readonly int currentSpeedHash = Animator.StringToHash("currentSpeed");

        public PlayerAnimationController(PlayerController _playerController, Animator _playerAnimator)
        {
            playerController = _playerController;
            playerAnimator = _playerAnimator;
        }

        public void Update()
        {
            UpdateAnimationParameters();
        }

        private void UpdateAnimationParameters()
        {
            playerAnimator.SetFloat(movementXHash, playerController.GetMovementDirection().x, 0.1f, Time.deltaTime);
            playerAnimator.SetFloat(movementZHash, playerController.GetMovementDirection().z, 0.1f, Time.deltaTime);
            playerAnimator.SetFloat(currentSpeedHash, playerController.GetCurrentSpeed(), 0.1f, Time.deltaTime);
        }

        // Setters
        public void SetAnimation(bool _isSmoothTransition)
        {
            switch (playerController.GetModel().PlayerState)
            {
                case PlayerState.IDLE:
                case PlayerState.WALK:
                    playerAnimator.Play(walkHash);
                    break;

                default:
                    playerAnimator.Play(tPoseHash);
                    break;
            }
        }
    }
}