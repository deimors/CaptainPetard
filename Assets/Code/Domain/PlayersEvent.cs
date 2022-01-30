using UnityEngine;

public abstract class PlayersEvent
{
	public class PlayerSpawned : PlayersEvent
	{
		public PlayerIdentifier PlayerId { get; }
		public PlayerConfig Config { get; }

		public PlayerSpawned(PlayerIdentifier playerId, PlayerConfig config)
		{
			PlayerId = playerId;
			Config = config;
		}
	}

	public class BombDropped : PlayersEvent
	{
		public PlayerIdentifier PlayerId { get; }
		public Vector2 Position { get; }
		public PlayerColours PlayerColour { get; }

		public BombDropped(PlayerIdentifier playerId, Vector2 position, PlayerColours playerColour)
		{
			PlayerId = playerId;
			Position = position;
			PlayerColour = playerColour;
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