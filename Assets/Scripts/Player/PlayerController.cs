using ServiceLocator.Controls;
using UnityEngine;

namespace ServiceLocator.Player
{
    public class PlayerController
    {
        // Private Variables
        private PlayerModel playerModel;
        private PlayerView playerView;
        private PlayerAnimationController playerAnimationController;

        private Vector3 movementDirection;
        private float verticalVelocity;
        private float currentSpeed;
        private float targetSpeed;

        // Private Services
        private InputService inputService;

        public PlayerController(PlayerData _playerData, PlayerView _playerPrefab,
            InputService _inputService)
        {
            // Setting Variables
            playerModel = new PlayerModel(_playerData);
            playerView = Object.Instantiate(_playerPrefab).GetComponent<PlayerView>();
            playerView.SetController(this);
            playerAnimationController = new PlayerAnimationController(this, playerView.GetAnimator());

            // Setting Services
            inputService = _inputService;

            // Setting Elements;
            SetPlayerState(PlayerState.IDLE, false);

            verticalVelocity = 0f;
            currentSpeed = 0f;
            targetSpeed = 0f;
        }

        public void Update()
        {
            HandleStates();
            HandleMovement();
            playerAnimationController.Update();
        }

        private void HandleStates()
        {
            if (IsGrounded())
            {
                if (inputService.GetPlayerMovement.magnitude > 0.1f)
                {
                    SetPlayerState(PlayerState.WALK);
                }
                else
                {
                    SetPlayerState(PlayerState.IDLE);
                }
            }
            else
            {
                SetPlayerState(PlayerState.FALL);
            }
        }

        private void HandleMovement()
        {
            movementDirection = new Vector3(inputService.GetPlayerMovement.x, 0f, inputService.GetPlayerMovement.y);

            if (playerModel.PlayerState == PlayerState.WALK)
            {
                float backwardMultiplier = movementDirection.z < -0.1f ? playerModel.BackwardMovementMultiplier : 1f;
                float sideMultiplier = Mathf.Abs(movementDirection.x) > 0.1f ? playerModel.BackwardMovementMultiplier : 1f;
                float speedMultiplier = (backwardMultiplier + sideMultiplier) / 2;
                targetSpeed = playerModel.MaxWalkSpeed * speedMultiplier;
            }
            else
            {
                targetSpeed = 0;
            }

            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime);

            ApplyGravity();

            playerView.GetCharacterController().Move(movementDirection * currentSpeed * Time.deltaTime);
        }

        private void ApplyGravity()
        {
            movementDirection.y = 0;

            if (playerModel.PlayerState == PlayerState.FALL)
            {
                // Apply gravity when in air
                verticalVelocity -= playerModel.GravityScale * Time.deltaTime;
            }
            else
            {
                // Resetting gravity when grounded
                verticalVelocity = -0.5f; // to keep player on the ground
            }

            movementDirection.y = verticalVelocity;
        }

        // Setters
        public void SetPlayerState(PlayerState _playerState, bool _isSmoothTransition = true)
        {
            playerModel.PlayerState = _playerState;
            playerAnimationController.SetAnimation(_isSmoothTransition);
        }

        // Getters
        public PlayerModel GetModel() => playerModel;
        public Vector3 GetMovementDirection() => movementDirection;
        public float GetCurrentSpeed() => currentSpeed;

        private bool IsGrounded()
        {
            float checkDistance = 0.2f;
            Vector3 origin = playerView.transform.position + Vector3.up * 0.1f;

            bool hitGround = Physics.Raycast(origin, Vector3.down, checkDistance, playerView.GetGroundLayerMask());

            Debug.DrawRay(origin, Vector3.down * checkDistance, hitGround ? Color.green : Color.red);

            return hitGround;
        }

    }
}