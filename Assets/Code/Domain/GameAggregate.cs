using System;
using UniRx;

public class GameAggregate : IGameEvents, IGameCommands
{
	private readonly Subject<GameEvent> _events = new();

	private int _score = 0;

	public IDisposable Subscribe(IObserver<GameEvent> observer)
		=> _events.Subscribe(observer);

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