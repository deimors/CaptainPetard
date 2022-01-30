using UnityEngine;

public class EnemyConfig
{
	public Vector2 Position { get; }
	public PlayerColours Colour { get; }

	public EnemyConfig(Vector2 position, PlayerColours colour)
	{
		Position = position;
		Colour = colour;
	}
}