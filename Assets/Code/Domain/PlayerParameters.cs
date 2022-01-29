using UnityEngine;

public class PlayerParameters
{
	public PlayerIdentifier PlayerId { get; }
	public Vector2 Position { get; }
	public PlayerInputAxes InputAxes { get; }

	public PlayerParameters(PlayerIdentifier playerId, Vector2 position, PlayerInputAxes inputAxes)
	{
		PlayerId = playerId;
		Position = position;
		InputAxes = inputAxes;
	}
}