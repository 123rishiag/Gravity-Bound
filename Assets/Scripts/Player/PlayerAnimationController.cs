using UnityEngine;

namespace ServiceLocator.Player
{
    public class PlayerAnimationController
    {
        // Private Variables
        private Animator playerAnimator;

        // Animation Hashes
        private readonly int tPoseHash = Animator.StringToHash("TPose");
        private readonly int idleHash = Animator.StringToHash("Idle");

        public PlayerAnimationController(Animator _playerAnimator) => playerAnimator = _playerAnimator;

        // Setters
        public void SetAnimation(PlayerState _playerState, bool _isSmoothTransition)
        {
            float transitionFactor = _isSmoothTransition ? 0.2f : 0f;
            switch (_playerState)
            {
                case PlayerState.IDLE:
                    playerAnimator.CrossFade(idleHash, transitionFactor);
                    break;
                default:
                    playerAnimator.CrossFade(tPoseHash, transitionFactor);
                    break;
            }
        }
    }
}