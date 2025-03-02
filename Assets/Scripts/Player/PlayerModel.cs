namespace ServiceLocator.Player
{
    public class PlayerModel
    {
        public PlayerModel(PlayerData _playerData)
        {
            PlayerState = PlayerState.IDLE;
            MaxWalkSpeed = _playerData.maxWalkSpeed;
            MaxRunSpeed = _playerData.maxRunSpeed;
            RotationSpeed = _playerData.rotationSpeed;
            GravityScale = _playerData.gravityScale;
        }

        // Getters & Setters
        public PlayerState PlayerState { get; set; }
        public float MaxWalkSpeed { get; private set; }
        public float MaxRunSpeed { get; private set; }
        public float RotationSpeed { get; private set; }
        public float GravityScale { get; private set; }
    }
}