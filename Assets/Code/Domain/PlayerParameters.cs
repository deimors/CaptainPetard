using UnityEngine;

public class PlayerParameters
{
	public PlayerIdentifier PlayerId { get; }
	public PlayerConfig Config { get; }

	public PlayerParameters(PlayerIdentifier playerId, PlayerConfig config)
	{
		PlayerId = playerId;
		Config = config;
	}
}