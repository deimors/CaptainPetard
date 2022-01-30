using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

public class EnemiesAggregate : IEnemiesEvents, IEnemiesCommands
{
	private readonly Subject<EnemiesEvent> _events = new();

	private readonly ISet<EnemyIdentifier> _enemies = new HashSet<EnemyIdentifier>();

	public IDisposable Subscribe(IObserver<EnemiesEvent> observer)
		=> _events.Subscribe(observer);
	
	public void AddEnemy(Vector2 position, PlayerColours colour)
	{
		var enemyId = new EnemyIdentifier();

		_enemies.Add(enemyId);

		_events.OnNext(new EnemiesEvent.EnemySpawned(enemyId, new EnemyConfig(position, colour)));
	}

	public void KillEnemy(EnemyIdentifier enemyId, int points)
	{
		if (!_enemies.Remove(enemyId))
			throw new ArgumentException($"{enemyId} not in enemies set");

		_events.OnNext(new EnemiesEvent.EnemyKilled(enemyId, points));

		if (!_enemies.Any())
		{
			Debug.Log("All enemies killed");
			_events.OnNext(new EnemiesEvent.AllEnemiesKilled());
		}
	}
}