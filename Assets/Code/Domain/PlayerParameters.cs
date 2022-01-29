using UnityEngine;

public class PlayerParameters
{
	public PlayerIdentifier PlayerId { get; }
	public Vector2 Position { get; }
	public PlayerInputAxes InputAxes { get; }
	public PlayerColours Colour { get; }

	public PlayerParameters(PlayerIdentifier playerId, Vector2 position, PlayerInputAxes inputAxes, PlayerColours colour)
	{
		PlayerId = playerId;
		Position = position;
		InputAxes = inputAxes;
		Colour = colour;
	}
}