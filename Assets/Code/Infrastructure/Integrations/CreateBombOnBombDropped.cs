using System;
using UniRx;
using Zenject;

public class CreateBombOnBombDropped : IDisposable
{
	private readonly IFactory<BombParameters, Unit> _bombFactory;
	private readonly IDisposable _disposable;

	public CreateBombOnBombDropped(IPlayersEvents playersEvents, IFactory<BombParameters, Unit> bombFactory)
	{
		_bombFactory = bombFactory;

		_disposable = playersEvents
			.OfType<PlayersEvent, PlayersEvent.BombDropped>()
			.Subscribe(CreateBomb);
	}

	private void CreateBomb(PlayersEvent.BombDropped @event)
	{
		var parameters = new BombParameters(
			@event.PlayerId,
			@event.Position,
			@event.PlayerColour
		);

		_bombFactory.Create(parameters);
	}

	public void Dispose()
	{
		_disposable.Dispose();
	}
}