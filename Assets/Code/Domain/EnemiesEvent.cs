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
		public int Points { get; }

		public EnemyKilled(EnemyIdentifier enemyId, int points)
		{
			EnemyId = enemyId;
			Points = points;
		}
	}

	public class AllEnemiesKilled : EnemiesEvent
	{
	}
}