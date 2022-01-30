using System;
using System.Collections.Generic;
using System.Linq;
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

	public void NewPlayer(PlayerIdentifier playerId, PlayerConfig config)
	{
		if (_playerStates.ContainsKey(playerId))
			throw new ArgumentException($"PlayerId {playerId} already added");

		var playerState = _initialState;

		_playerStates.Add(playerId, playerState);
		_playerConfigs.Add(playerId, config);

		_events.OnNext(new PlayersEvent.PlayerAdded(playerId, playerState.LifeCount));
		_events.OnNext(new PlayersEvent.PlayerSpawned(playerId, config));
	}

	public void DropBomb(PlayerIdentifier playerId, Vector2 position)
	{
		var playerState = _playerStates[playerId];

		if (playerState.BombCount > 0)
		{
			_playerStates[playerId] = playerState.UseBomb();
			var config = _playerConfigs[playerId];

			_events.OnNext(new PlayersEvent.BombDropped(playerId, position, config.Colour));
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
			playerState = playerState.Kill();

			_playerStates[playerId] = playerState;

			_events.OnNext(new PlayersEvent.PlayerKilled(playerId, playerState.LifeCount));

			Observable.Timer(TimeSpan.FromSeconds(2))
				.Subscribe(_ => RevivePlayer(playerId))
				.AddTo(_disposable);

			if (NoPlayersHaveRemainingLife)
			{
				_events.OnNext(new PlayersEvent.NoLivesRemaining());
			}
		}
	}

	private bool NoPlayersHaveRemainingLife 
		=> _playerStates.All(kvp => kvp.Value.LifeCount == 0);

	private void RevivePlayer(PlayerIdentifier playerId)
	{
		var playerState = _playerStates[playerId];

		if (!playerState.Alive && playerState.LifeCount > 0)
		{
			playerState = playerState.Revive();

			_playerStates[playerId] = playerState;
			var config = _playerConfigs[playerId];

			_events.OnNext(new PlayersEvent.PlayerSpawned(playerId, config));
		}
	}

	public void Dispose()
	{
		_disposable.Dispose();
	}
}