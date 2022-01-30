using System;
using UnityEngine;

public enum PlayerColours
{
	Red,
	Blue
}

public static class PlayerColoursExtensions
{
	private static readonly Color Red = new Color(1, 0, 0);
	private static readonly Color Blue = new Color((float)45/255, (float)155/255, 1);

	public static Color ToColor(this PlayerColours colour)
	{
		switch (colour)
		{
			case PlayerColours.Red:
				return Red;
			case PlayerColours.Blue:
				return Blue;
			default:
				throw new ArgumentOutOfRangeException(nameof(colour), colour, null);
		}
	}
}