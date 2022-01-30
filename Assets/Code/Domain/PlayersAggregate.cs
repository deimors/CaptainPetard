using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayersAggregate : IPlayersEvents, IPlayersCommands, IDisposable
{
	private readonly CompositeDisposable _disposable = new CompositeDisposable();

	private readonly Subject<PlayersEvent> _events = new Subject<PlayersEvent>();
	private readonly IDictionary<PlayerIdentifier, PlayerState> _playerStates = new Dictionary<PlayerIdentifier, PlayerState>();
	private readonly IDictionary<PlayerIdentifier, PlayerConfig> _playerConfigs = new Dictionary<PlayerIdentifier, PlayerConfig>();

	private readonly PlayerState _initialState = new PlayerState(1, 3, true);

	public IDisposable Subscribe(IObserver<PlayersEvent> observer)
		=> _events.Subscribe(observer);

	public void NewPlayer(Vector2 position, PlayerInputAxes inputAxes, PlayerColours colour)
	{
		var playerId = PlayerIdentifier.Create();

		_playerStates.Add(playerId, _initialState);
		_playerConfigs.Add(playerId, new PlayerConfig(position, inputAxes, colour));
		
		_events.OnNext(new PlayersEvent.PlayerSpawned(playerId, position, inputAxes, colour));
	}

	public void DropBomb(PlayerIdentifier playerId, Vector2 position)
	{
		var playerState = _playerStates[playerId];

		if (playerState.BombCount > 0)
		{
			_playerStates[playerId] = playerState.UseBomb();
			_events.OnNext(new PlayersEvent.BombDropped(playerId, position));
		}
	}

	public void ReturnBomb(PlayerIdentifier playerId)
	{
		_playerStates[playerId] = _playerStates[playerId].AddBomb();
		_events.OnNext(new PlayersEvent.BombReturned(playerId));
	}

	public void KillPlayer(PlayerIdentifier playerId)
	{
		var playerState = _playerStates[playerId];

		if (playerState.Alive)
		{
			_playerStates[playerId] = playerState.Kill();
			_events.OnNext(new PlayersEvent.PlayerKilled(playerId));

			Observable.Timer(TimeSpan.FromSeconds(2))
				.Subscribe(_ => RevivePlayer(playerId))
				.AddTo(_disposable);
		}
	}

	private void RevivePlayer(PlayerIdentifier playerId)
	{
		var playerState = _playerStates[playerId];

		if (!playerState.Alive && playerState.LifeCount > 0)
		{
			_playerStates[playerId] = playerState.Revive();
			var playerConfig = _playerConfigs[playerId];

			_events.OnNext(new PlayersEvent.PlayerSpawned(playerId, playerConfig.Position, playerConfig.InputAxes, playerConfig.Colour));
		}
	}

	public void Dispose()
	{
		_disposable.Dispose();
	}
}