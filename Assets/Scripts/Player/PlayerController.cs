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

        private Vector2 movementInput;
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
            if (IsGrounded())
            {
                if (inputService.GetPlayerMovement.magnitude > 0.1f)
                {
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
            RotatePlayer();
            playerView.GetCharacterController().Move(movementDirection * currentSpeed * Time.deltaTime);
        }

        private void SetDirection()
        {
            movementInput = inputService.GetPlayerMovement;
            Vector3 inputVector = new Vector3(movementInput.x, 0f, movementInput.y);

            // Converting input direction to world space based on player's rotation
            Quaternion cameraRotation = Camera.main.transform.rotation;
            cameraRotation.x = 0f; // To ignore Vertical tilt
            cameraRotation.z = 0f;

            // Updated Movement direction is rotation of camera * input Vector
            Vector3 targetDirection = cameraRotation * inputVector;
            movementDirection = Vector3.Lerp(movementDirection, targetDirection, Time.deltaTime);
        }

        private void SetSpeed()
        {
            float targetSpeed = 0f;

            if (playerModel.PlayerState == PlayerState.WALK || playerModel.PlayerState == PlayerState.RUN)
            {
                targetSpeed =
                    playerModel.PlayerState == PlayerState.RUN ? playerModel.MaxRunSpeed : playerModel.MaxWalkSpeed;
            }
            else
            {
                targetSpeed = 0f;
            }

            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime);
        }

        private void ApplyGravity()
        {
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

        private void RotatePlayer()
        {
            if (playerModel.PlayerState == PlayerState.WALK || playerModel.PlayerState == PlayerState.RUN)
            {
                Vector3 lookDirection = movementDirection;
                lookDirection.y = 0f; // To avoid Rotation on vertical tilt

                if (lookDirection.sqrMagnitude > 0.01f)
                {
                    float angleBetween = Vector3.Angle(playerView.transform.forward, lookDirection);

                    float rotationSpeed = playerModel.RotationSpeed;

                    // If angle is greater than 120 degree
                    if (angleBetween > 120f)
                    {
                        rotationSpeed *= 0.5f;
                    }

                    Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                    playerView.transform.rotation = Quaternion.Slerp(
                        playerView.transform.rotation,
                        targetRotation,
                        rotationSpeed * Time.deltaTime
                    );
                }
            }
        }
        public void SetPlayerState(PlayerState _playerState)
        {
            playerModel.PlayerState = _playerState;
            playerAnimationController.SetAnimation();
        }

        // Getters
        public PlayerModel GetModel() => playerModel;
        public Transform GetTransform() => playerView.transform;
        public Vector2 GetMovementInput() => movementInput;
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