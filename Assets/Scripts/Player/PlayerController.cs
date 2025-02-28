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
        private Vector3 lastMovementDirection;
        private float verticalVelocity;
        private float currentSpeed;
        private float speedMultiplier;

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

            lastMovementDirection = Vector3.zero;
            verticalVelocity = 0f;
            currentSpeed = 0f;
            speedMultiplier = 0f;
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
                    if (inputService.IsPlayerRunning)
                        SetPlayerState(PlayerState.RUN);
                    else
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
            SetDirection();
            SetSpeed();
            ApplyGravity();
            playerView.GetCharacterController().Move(movementDirection * currentSpeed * Time.deltaTime);
        }

        private void ApplyGravity()
        {
            movementDirection.y = 0;

            if (playerModel.PlayerState == PlayerState.FALL)
            {
                // Applying gravity when in air
                verticalVelocity -= playerModel.GravityScale * Time.deltaTime;
            }
            else
            {
                // Resetting gravity when grounded
                verticalVelocity = -0.5f; // to keep player on the ground
            }

            movementDirection.y = verticalVelocity;
        }

        private void SetDirection()
        {
            // Setting Direction
            movementDirection = new Vector3(inputService.GetPlayerMovement.x, 0f, inputService.GetPlayerMovement.y);

            if (playerModel.PlayerState == PlayerState.WALK || playerModel.PlayerState == PlayerState.RUN)
            {
                lastMovementDirection = movementDirection;
            }
            else
            {
                movementDirection = lastMovementDirection;
            }
        }

        private void SetSpeed()
        {
            float targetSpeed = 0f;

            if (playerModel.PlayerState == PlayerState.WALK || playerModel.PlayerState == PlayerState.RUN)
            {

                // Setting Speed and its multiplier based on movement direction
                float backwardMultiplier = movementDirection.z < -0.1f ? playerModel.BackwardMovementMultiplier : 1f;
                float sideMultiplier = Mathf.Abs(movementDirection.x) > 0.1f ? playerModel.SideMovementMultiplier : 1f;
                speedMultiplier = Mathf.Min(backwardMultiplier, sideMultiplier);

                float maxSpeed =
                    playerModel.PlayerState == PlayerState.RUN ? playerModel.MaxRunSpeed : playerModel.MaxWalkSpeed;
                targetSpeed = maxSpeed * speedMultiplier;
            }
            else
            {
                targetSpeed = 0f;
            }

            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, playerModel.MovementTransitionFactor * Time.deltaTime);
        }

        // Setters
        public void SetPlayerState(PlayerState _playerState, bool _isSmoothTransition = true)
        {
            playerModel.PlayerState = _playerState;
            playerAnimationController.SetAnimation(_isSmoothTransition);
        }

        // Getters
        public PlayerModel GetModel() => playerModel;
        public Transform GetTransform() => playerView.transform;
        public Vector3 GetMovementDirection() => movementDirection;
        public float GetCurrentSpeed() => currentSpeed;
        public float GetSpeedMutiplier() => speedMultiplier;
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