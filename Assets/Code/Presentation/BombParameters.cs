using UnityEngine;

public class BombParameters
{
	public PlayerIdentifier OwnerId { get; }
	public Vector2 Position { get; }
	public PlayerColours PlayerColour { get; }

	public BombParameters(PlayerIdentifier ownerId, Vector2 position, PlayerColours playerColour)
	{
		OwnerId = ownerId;
		Position = position;
		PlayerColour = playerColour;
	}
}