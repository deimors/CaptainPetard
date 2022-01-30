using UnityEngine;

public readonly struct PlayerConfig
{
	public Vector2 Position { get; }
	public PlayerInputAxes InputAxes { get; }
	public PlayerColours Colour { get; }

	public PlayerConfig(Vector2 position, PlayerInputAxes inputAxes, PlayerColours colour)
	{
		Position = position;
		InputAxes = inputAxes;
		Colour = colour;
	}
}