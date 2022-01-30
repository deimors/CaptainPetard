using System;

public static class BlockColoursExtensions
{
	public static bool CanBeDestroyedBy(this BlockColours blockColour, PlayerColours playerColour)
	{
		switch (blockColour)
		{
			case BlockColours.Red:
				return playerColour == PlayerColours.Red;
			case BlockColours.Blue:
				return playerColour == PlayerColours.Blue;
			case BlockColours.Neutral:
				return true;
			default:
				throw new ArgumentOutOfRangeException(nameof(blockColour), blockColour, null);
		}
	}
}