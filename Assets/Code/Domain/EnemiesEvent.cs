public abstract class EnemiesEvent
{
	public class EnemySpawned : EnemiesEvent
	{
		public EnemyIdentifier EnemyId { get; }
		public EnemyConfig Config { get; }

		public EnemySpawned(EnemyIdentifier enemyId, EnemyConfig config)
		{
			EnemyId = enemyId;
			Config = config;
		}
	}
}