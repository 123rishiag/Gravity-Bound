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
            SetPlayerState(PlayerState.IDLE);

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
            if ((playerModel.PlayerState == PlayerState.WALK_TURN || playerModel.PlayerState == PlayerState.RUN_TURN) &&
                playerAnimationController.IsAnimationFinished())
            {
                SetPlayerState(PlayerState.IDLE);
            }
            else if (IsGrounded())
            {
                if (inputService.GetPlayerMovement.magnitude > 0.1f)
                {
                    // If instant move in opposite direction
                    if (Mathf.Sign(movementDirection.z) != Mathf.Sign(inputService.GetPlayerMovement.y) &&
                        Mathf.Abs(inputService.GetPlayerMovement.y) > 0.1f)
                    {
                        if (inputService.IsPlayerRunning)
                            SetPlayerState(PlayerState.RUN_TURN);
                        else
                            SetPlayerState(PlayerState.WALK_TURN);
                    }
                    else
                    {
                        if (inputService.IsPlayerRunning)
                            SetPlayerState(PlayerState.RUN);
                        else
                            SetPlayerState(PlayerState.WALK);
                    }
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
            Vector3 newInput = new Vector3(inputService.GetPlayerMovement.x, 0f, inputService.GetPlayerMovement.y);

            // Converting input direction to world space based on player's rotation
            Vector3 worldDirection = playerView.transform.rotation * newInput;

            // Applying smooth transition for X movement
            movementDirection.x = Mathf.Lerp(movementDirection.x, worldDirection.x, Time.deltaTime);

            // Applying smooth transition for Z movement
            movementDirection.z = Mathf.Lerp(movementDirection.z, worldDirection.z, Time.deltaTime);
        }

        private void SetSpeed()
        {
            float targetSpeed = 0f;

            if (playerModel.PlayerState == PlayerState.WALK || playerModel.PlayerState == PlayerState.RUN)
            {
                // Setting Speed and its multiplier based on movement direction
                speedMultiplier = Mathf.Abs(movementDirection.x) > 0.1f ? playerModel.SideMovementMultiplier : 1f;

                float maxSpeed =
                    playerModel.PlayerState == PlayerState.RUN ? playerModel.MaxRunSpeed : playerModel.MaxWalkSpeed;
                targetSpeed = maxSpeed * speedMultiplier;
            }
            else
            {
                targetSpeed = 0f;
            }

            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime);
        }

        // Setters
        public void SetPlayerState(PlayerState _playerState)
        {
            playerModel.PlayerState = _playerState;
            playerAnimationController.SetAnimation();
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