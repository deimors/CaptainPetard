using UnityEngine;

public interface IPlayersCommands
{
	void NewPlayer(Vector2 position);
	void DropBomb(PlayerIdentifier playerId, Vector2 position);
	void ReturnBomb(PlayerIdentifier playerId);
}