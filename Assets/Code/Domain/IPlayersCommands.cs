using UnityEngine;

public interface IPlayersCommands
{
	void NewPlayer(PlayerConfig config);
	void DropBomb(PlayerIdentifier playerId, Vector2 position);
	void ReturnBomb(PlayerIdentifier playerId);
	void KillPlayer(PlayerIdentifier playerId);
}