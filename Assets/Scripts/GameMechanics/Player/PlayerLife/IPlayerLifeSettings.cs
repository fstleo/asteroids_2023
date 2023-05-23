namespace Asteroids.GameMechanics.Player.PlayerLife
{
    public interface IPlayerLifeSettings
    {
        int LivesCount { get; }
        int RespawnSeconds { get; }
    }
}