using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class PlayersAggregate : IPlayersEvents, IPlayerCommands
{
	private readonly Subject<PlayersEvent> _events = new Subject<PlayersEvent>();
	private readonly IDictionary<PlayerIdentifier, int> _bombCounts = new Dictionary<PlayerIdentifier, int>();

	public IDisposable Subscribe(IObserver<PlayersEvent> observer)
		=> _events.Subscribe(observer);

	public void NewPlayer(Vector2 position)
	{
		var playerId = PlayerIdentifier.Create();

		_bombCounts.Add(playerId, 1);
		_events.OnNext(new PlayersEvent.NewPlayerCreated(playerId, position));
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
	void NewPlayer(Vector2 position);
	void DropBomb(PlayerIdentifier playerId, Vector2 position);
	void ReturnBomb(PlayerIdentifier playerId);
}

public class PlayerIdentifier : IEquatable<PlayerIdentifier>
{
	private readonly Guid _value;

	private PlayerIdentifier(Guid value)
	{
		_value = value;
	}

	public static PlayerIdentifier Create()
		=> new(Guid.NewGuid());

	public bool Equals(PlayerIdentifier other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return _value.Equals(other._value);
	}

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((PlayerIdentifier) obj);
	}

	public override int GetHashCode()
	{
		return _value.GetHashCode();
	}

	public static bool operator ==(PlayerIdentifier left, PlayerIdentifier right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(PlayerIdentifier left, PlayerIdentifier right)
	{
		return !Equals(left, right);
	}
}

public abstract class PlayersEvent
{
	public class NewPlayerCreated : PlayersEvent
	{
		public PlayerIdentifier PlayerId { get; }
		public Vector2 Position { get; }

		public NewPlayerCreated(PlayerIdentifier playerId, Vector2 position)
		{
			PlayerId = playerId;
			Position = position;
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

public class PlayerFactory : IFactory<PlayerParameters, Unit>
{
	private readonly DiContainer _container;
	private readonly GameObject _prefab;
	private readonly Transform _parent;

	public PlayerFactory(DiContainer container, GameObject prefab, Transform parent)
	{
		_container = container;
		_prefab = prefab;
		_parent = parent;
	}

	public Unit Create(PlayerParameters param)
	{
		var subContainer = _container.CreateSubContainer();

		subContainer.BindInstance(param.PlayerId);

		subContainer.InstantiatePrefab(_prefab, _parent);

		return Unit.Default;
	}
}

public class PlayerParameters
{
	public PlayerIdentifier PlayerId { get; }
	public Vector2 Position { get; }

	public PlayerParameters(PlayerIdentifier playerId, Vector2 position)
	{
		PlayerId = playerId;
		Position = position;
	}
}
