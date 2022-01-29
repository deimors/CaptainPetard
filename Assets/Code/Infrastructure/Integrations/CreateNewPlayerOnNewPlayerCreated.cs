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
			@event.Position
		);

		_playerFactory.Create(parameters);
	}

	public void Dispose()
	{
		_disposable.Dispose();
	}
}

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
			@event.Position
		);

		_bombFactory.Create(parameters);
	}

	public void Dispose()
	{
		_disposable.Dispose();
	}
}
