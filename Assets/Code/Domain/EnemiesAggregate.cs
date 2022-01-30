using System;
using UniRx;
using UnityEngine;

public class EnemiesAggregate : IEnemiesEvents, IEnemiesCommands
{
	private readonly Subject<EnemiesEvent> _events = new();

	public IDisposable Subscribe(IObserver<EnemiesEvent> observer)
		=> _events.Subscribe(observer);

	public void AddEnemy(Vector2 position, PlayerColours colour)
	{
		_events.OnNext(new EnemiesEvent.EnemySpawned(new EnemyIdentifier(), new EnemyConfig(position, colour)));
	}
}