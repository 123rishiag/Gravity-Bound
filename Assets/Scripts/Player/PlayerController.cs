using UnityEngine;

namespace ServiceLocator.Player
{
    public class PlayerController
    {
        // Private Variables
        private PlayerModel playerModel;
        private PlayerView playerView;
        private PlayerAnimationController playerAnimationController;

        private PlayerState playerState;

        public PlayerController(PlayerView _playerPrefab)
        {
            // Setting Variables
            playerModel = new PlayerModel();
            playerView = Object.Instantiate(_playerPrefab).GetComponent<PlayerView>();
            playerView.SetController(this);
            playerAnimationController = new PlayerAnimationController(playerView.GetAnimator());

            // Setting Elements;
            SetPlayerState(PlayerState.IDLE, false);
        }

        // Setters
        public void SetPlayerState(PlayerState _playerState, bool _isSmoothTransition = true)
        {
            playerState = _playerState;
            playerAnimationController.SetAnimation(_playerState, _isSmoothTransition);
        }
    }
}