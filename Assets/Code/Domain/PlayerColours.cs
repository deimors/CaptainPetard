using System;
using UnityEngine;

public enum PlayerColours
{
	Red,
	Blue
}

public static class PlayerColoursExtensions
{
	public static Color GetColor(this PlayerColours colour)
	{
		switch (colour)
		{
			case PlayerColours.Red:
				return Color.red;
			case PlayerColours.Blue:
				return Color.blue;
			default:
				throw new ArgumentOutOfRangeException(nameof(colour), colour, null);
		}
	}
}