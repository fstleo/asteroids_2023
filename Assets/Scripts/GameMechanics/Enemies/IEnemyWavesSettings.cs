namespace Asteroids.GameMechanics.Enemies
{
    public interface IEnemyWavesSettings
    {
        EnemyWave GetWave(int index);
    }
}