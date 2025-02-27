using UnityEngine;

namespace ServiceLocator.Controls
{
    public class InputService
    {
        // Private Variables
        private InputControls inputControls;

        public InputService() => inputControls = new InputControls();
        public void Init() => inputControls.Enable();
        public void Destroy() => inputControls.Disable();
        public void Update()
        {
            FetchPlayerMovement();
            IsPlayerRunning = inputControls.Player.IsRunning.IsPressed();
        }

        private void FetchPlayerMovement()
        {
            Vector2 rawInput = inputControls.Player.Move.ReadValue<Vector2>();

            // Rounding values only when input is pressed, else 0
            float inputX = Mathf.Abs(rawInput.x) > 0.1f ? Mathf.Round(rawInput.x) : 0;
            float inputY = Mathf.Abs(rawInput.y) > 0.1f ? Mathf.Round(rawInput.y) : 0;

            GetPlayerMovement = new Vector2(inputX, inputY);
        }

        public Vector2 GetPlayerMovement { get; private set; }
        public bool IsPlayerRunning { get; private set; }
    }
}