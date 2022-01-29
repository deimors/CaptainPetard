using UnityEngine;

public class BombParameters
{
	public PlayerIdentifier OwnerId { get; }
	public Vector2 Position { get; }

	public BombParameters(PlayerIdentifier ownerId, Vector2 position)
	{
		OwnerId = ownerId;
		Position = position;
	}
}