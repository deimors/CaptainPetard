using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayersAggregate : IPlayersEvents, IPlayersCommands
{
	private readonly Subject<PlayersEvent> _events = new Subject<PlayersEvent>();
	private readonly IDictionary<PlayerIdentifier, int> _bombCounts = new Dictionary<PlayerIdentifier, int>();

	public IDisposable Subscribe(IObserver<PlayersEvent> observer)
		=> _events.Subscribe(observer);

	public void NewPlayer(Vector2 position, PlayerInputAxes inputAxes)
	{
		var playerId = PlayerIdentifier.Create();

		_bombCounts.Add(playerId, 1);
		_events.OnNext(new PlayersEvent.NewPlayerCreated(playerId, position, inputAxes));
	}

	public void DropBomb(PlayerIdentifier playerId, Vector2 position)
	{
		if (_bombCounts.TryGetValue(playerId, out var bombCount) && bombCount > 0)
		{
			_bombCounts[playerId] = bombCount - 1;
			_events.OnNext(new PlayersEvent.BombDropped(playerId, position));
		}
	}

	public void ReturnBomb(PlayerIdentifier playerId)
	{
		_bombCounts[playerId] += 1;
		_events.OnNext(new PlayersEvent.BombReturned(playerId));
	}
}