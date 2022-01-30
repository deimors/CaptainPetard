using System;
using UniRx;
using Zenject;

public class CreateNewPlayerOnPlayerSpawned : IDisposable
{
	private readonly IFactory<PlayerParameters, Unit> _playerFactory;
	private readonly IDisposable _disposable;

	public CreateNewPlayerOnPlayerSpawned(IPlayersEvents playersEvents, IFactory<PlayerParameters, Unit> playerFactory)
	{
		_playerFactory = playerFactory;

		_disposable = playersEvents
			.OfType<PlayersEvent, PlayersEvent.PlayerSpawned>()
			.Subscribe(CreateNewPlayer);
	}

	private void CreateNewPlayer(PlayersEvent.PlayerSpawned @event)
	{
		var parameters = new PlayerParameters(
			@event.PlayerId,
			@event.Config
		);

		_playerFactory.Create(parameters);
	}

	public void Dispose()
	{
		_disposable.Dispose();
	}
}