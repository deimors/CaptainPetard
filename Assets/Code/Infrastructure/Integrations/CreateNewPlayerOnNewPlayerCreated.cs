using System;
using UniRx;
using Zenject;

public class CreateNewPlayerOnNewPlayerCreated : IDisposable
{
	private readonly IFactory<PlayerParameters, Unit> _playerFactory;
	private readonly IDisposable _disposable;

	public CreateNewPlayerOnNewPlayerCreated(IPlayersEvents playersEvents, IFactory<PlayerParameters, Unit> playerFactory)
	{
		_playerFactory = playerFactory;

		_disposable = playersEvents
			.OfType<PlayersEvent, PlayersEvent.NewPlayerCreated>()
			.Subscribe(CreateNewPlayer);
	}

	private void CreateNewPlayer(PlayersEvent.NewPlayerCreated @event)
	{
		var parameters = new PlayerParameters(
			@event.PlayerId,
			@event.Position,
			@event.InputAxes
		);

		_playerFactory.Create(parameters);
	}

	public void Dispose()
	{
		_disposable.Dispose();
	}
}