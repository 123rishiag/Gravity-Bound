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
            GetPlayerMovement = inputControls.Player.Move.ReadValue<Vector2>();
        }

        public Vector2 GetPlayerMovement { get; private set; }
    }
}