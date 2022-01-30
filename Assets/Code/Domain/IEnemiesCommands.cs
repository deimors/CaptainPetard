using UnityEngine;

public interface IEnemiesCommands
{
	void AddEnemy(Vector2 position, PlayerColours colour);

	void KillEnemy(EnemyIdentifier enemyId);
}