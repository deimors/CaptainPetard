using System;
using UniRx;
using Unity.VisualScripting;
using Zenject;

public class GameAggregate : IGameEvents, IGameCommands
{
	private readonly Subject<GameEvent> _events = new();

	private int _score = 0;

	public IDisposable Subscribe(IObserver<GameEvent> observer)
		=> _events.Subscribe(observer);
	
	void Start()
	{
		_events.OnNext(new GameEvent.ScoreChanged(_score));
	}

	public void GameOver()
	{
		_events.OnNext(new GameEvent.GameOver());
	}

	public void AddToScore(int value)
	{
		_score += value;

		_events.OnNext(new GameEvent.ScoreChanged(_score));
	}
}