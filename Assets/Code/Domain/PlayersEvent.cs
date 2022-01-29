using UnityEngine;

public abstract class PlayersEvent
{
	public class NewPlayerCreated : PlayersEvent
	{
		public PlayerIdentifier PlayerId { get; }
		public Vector2 Position { get; }
		public PlayerInputAxes InputAxes { get; }

		public NewPlayerCreated(PlayerIdentifier playerId, Vector2 position, PlayerInputAxes inputAxes)
		{
			PlayerId = playerId;
			Position = position;
			InputAxes = inputAxes;
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