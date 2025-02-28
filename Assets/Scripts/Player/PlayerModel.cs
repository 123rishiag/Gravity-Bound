namespace ServiceLocator.Player
{
    public class PlayerModel
    {
        public PlayerModel(PlayerData _playerData)
        {
            PlayerState = PlayerState.IDLE;
            MaxWalkSpeed = _playerData.maxWalkSpeed;
            MaxRunSpeed = _playerData.maxRunSpeed;
            BackwardMovementMultiplier = _playerData.backwardMovementMultiplier;
            SideMovementMultiplier = _playerData.sideMovementMultiplier;
            GravityScale = _playerData.gravityScale;
        }

        // Getters & Setters
        public PlayerState PlayerState { get; set; }
        public float MaxWalkSpeed { get; private set; }
        public float MaxRunSpeed { get; private set; }
        public float BackwardMovementMultiplier { get; private set; }
        public float SideMovementMultiplier { get; private set; }
        public float GravityScale { get; private set; }
    }
}