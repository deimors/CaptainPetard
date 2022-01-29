using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayersAggregate : IPlayersEvents, IPlayerCommands
{
	private readonly Subject<PlayersEvent> _events = new Subject<PlayersEvent>();
	private readonly IDictionary<PlayerIdentifier, int> _bombCounts = new Dictionary<PlayerIdentifier, int>();

	public IDisposable Subscribe(IObserver<PlayersEvent> observer)
		=> _events.Subscribe(observer);

	public void Add(PlayerIdentifier playerId)
	{
		_bombCounts.Add(playerId, 1);
		_events.OnNext(new PlayersEvent.PlayerAdded(playerId));
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

public interface IPlayersEvents : IObservable<PlayersEvent> {}

public interface IPlayerCommands
{
	void Add(PlayerIdentifier playerId);
	void DropBomb(PlayerIdentifier playerId, Vector2 position);
	void ReturnBomb(PlayerIdentifier playerId);
}

public class PlayerIdentifier
{
}

public abstract class PlayersEvent
{
	public class PlayerAdded : PlayersEvent
	{
		public PlayerIdentifier PlayerId { get; }

		public PlayerAdded(PlayerIdentifier playerId)
		{
			PlayerId = playerId;
		}
	}

	public class BombDropped : PlayersEvent
	{
		public PlayerIdentifier PlayerId { get; }
		public Vector2 Position { get; }

		public BombDropped(PlayerIdentifier playerId, Vector2 position)
		{
			PlayerId = playerId;
			Position = position;
		}
	}

	public class BombReturned : PlayersEvent
	{
		public PlayerIdentifier PlayerId { get; }

		public BombReturned(PlayerIdentifier playerId)
		{
			PlayerId = playerId;
		}
	}
}
