using System;
using UniRx;
using Zenject;

public class CreateNewEnemyOnEnemySpawned : IDisposable
{
	private readonly IDisposable _disposable;

	public CreateNewEnemyOnEnemySpawned(IEnemiesEvents enemiesEvents, IFactory<EnemyParameters, Unit> enemiesFactory)
	{
		_disposable = enemiesEvents
			.OfType<EnemiesEvent, EnemiesEvent.EnemySpawned>()
			.Subscribe(enemySpawned => enemiesFactory.Create(new EnemyParameters(enemySpawned.EnemyId,
				enemySpawned.Config.Position, enemySpawned.Config.Colour)));
	}

	public void Dispose()
	{
		_disposable.Dispose();
	}
}