using System;
using UniRx;
using UnityEngine;

public class AddScoreOnEnemyKilled : IDisposable
{
	private readonly IDisposable _disposable;

	public AddScoreOnEnemyKilled(IEnemiesEvents enemiesEvents, IGameCommands gameCommands)
	{
		_disposable = enemiesEvents
			.OfType<EnemiesEvent, EnemiesEvent.EnemyKilled>()
			.Subscribe(@event =>gameCommands.AddToScore(@event.Points));
	}

	public void Dispose()
	{
		_disposable.Dispose();
	}
	
}
