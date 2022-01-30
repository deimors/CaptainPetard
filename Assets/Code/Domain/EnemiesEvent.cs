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

	public class EnemyKilled : EnemiesEvent
	{
		public EnemyIdentifier EnemyId { get; }

		public EnemyKilled(EnemyIdentifier enemyId)
		{
			EnemyId = enemyId;
		}
	}

	public class AllEnemiesKilled : EnemiesEvent
	{
	}
}