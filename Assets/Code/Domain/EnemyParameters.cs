using UnityEngine;

public class EnemyParameters
{
	public EnemyIdentifier EnemyId { get; }
	public Vector2 Position { get; }
	public PlayerColours Colour { get; }

	public EnemyParameters(EnemyIdentifier enemyId, Vector2 position, PlayerColours colour)
	{
		EnemyId = enemyId;
		Position = position;
		Colour = colour;
	}
}