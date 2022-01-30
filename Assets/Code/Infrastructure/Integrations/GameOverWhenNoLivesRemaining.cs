using System;
using UniRx;

public class GameOverWhenNoLivesRemaining : IDisposable
{
	private readonly IDisposable _disposable;

	public GameOverWhenNoLivesRemaining(IPlayersEvents playersEvents, IGameCommands gameCommands)
	{
		_disposable = playersEvents
			.OfType<PlayersEvent, PlayersEvent.NoLivesRemaining>()
			.Subscribe(_ => gameCommands.GameOver());
	}

	public void Dispose()
	{
		_disposable.Dispose();
	}
}