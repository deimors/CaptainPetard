using UnityEngine;

public interface IPlayersCommands
{
	void NewPlayer(Vector2 position, PlayerInputAxes inputAxes, PlayerColours colour);
	void DropBomb(PlayerIdentifier playerId, Vector2 position);
	void ReturnBomb(PlayerIdentifier playerId);
	void KillPlayer(PlayerIdentifier playerId);
}