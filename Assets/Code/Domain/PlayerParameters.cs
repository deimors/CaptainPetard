using UnityEngine;

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