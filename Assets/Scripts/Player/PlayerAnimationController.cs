using UnityEngine;

namespace ServiceLocator.Player
{
    public class PlayerAnimationController
    {
        // Private Variables
        private PlayerController playerController;
        private Animator playerAnimator;

        private float movementSpeed;

        // Animation Hashes
        private readonly int tPoseHash = Animator.StringToHash("TPose");
        private readonly int moveHash = Animator.StringToHash("Move");
        private readonly int walkTurnHash = Animator.StringToHash("Walk_Turn");
        private readonly int runTurnHash = Animator.StringToHash("Run_Turn");

        // Parameter Hashes
        private readonly int movementXHash = Animator.StringToHash("movementX");
        private readonly int movementZHash = Animator.StringToHash("movementZ");
        private readonly int speedMultiplierHash = Animator.StringToHash("speedMultiplier");

        public PlayerAnimationController(PlayerController _playerController, Animator _playerAnimator)
        {
            playerController = _playerController;
            playerAnimator = _playerAnimator;
        }

        public void Update() => UpdateAnimationParameters();

        private void UpdateAnimationParameters()
        {
            UpdateMovementParameters();
        }

        private void UpdateMovementParameters()
        {
            PlayerState playerState = playerController.GetModel().PlayerState;

            // Fetching Animation Factors for movement
            float targetSpeed;
            switch (playerState)
            {
                case PlayerState.RUN:
                    targetSpeed = 1f;
                    break;
                case PlayerState.WALK:
                    targetSpeed = 0.5f;
                    break;
                case PlayerState.IDLE:
                    targetSpeed = 0f;
                    break;
                default:
                    targetSpeed = -1f;
                    break;
            }
            movementSpeed = Mathf.Lerp(movementSpeed, targetSpeed, Time.deltaTime);
            playerAnimator.SetFloat(speedMultiplierHash, movementSpeed);
        }

        // Setters
        public void SetAnimation()
        {
            switch (playerController.GetModel().PlayerState)
            {
                case PlayerState.IDLE:
                case PlayerState.WALK:
                case PlayerState.RUN:
                    playerAnimator.Play(moveHash);
                    break;

                default:
                    playerAnimator.Play(tPoseHash);
                    break;
            }
        }

        // Getters
        public bool IsAnimationFinished()
        {
            AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.normalizedTime >= 1.0f)
            {
                return true;
            }
            return false;
        }
    }
}