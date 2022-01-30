using UnityEngine;

public abstract class PlayersEvent
{
	public class PlayerSpawned : PlayersEvent
	{
		public PlayerIdentifier PlayerId { get; }
		public Vector2 Position { get; }
		public PlayerInputAxes InputAxes { get; }
		public PlayerColours Colour { get; }

		public PlayerSpawned(PlayerIdentifier playerId, Vector2 position, PlayerInputAxes inputAxes, PlayerColours colour)
		{
			PlayerId = playerId;
			Position = position;
			InputAxes = inputAxes;
			Colour = colour;
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

	public class PlayerKilled : PlayersEvent
	{
		public PlayerIdentifier PlayerId { get; }

		public PlayerKilled(PlayerIdentifier playerId)
		{
			PlayerId = playerId;
		}
	}
}