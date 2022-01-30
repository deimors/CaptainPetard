using System;

public class PlayerIdentifier : IEquatable<PlayerIdentifier>
{
	public readonly int Value;

	public PlayerIdentifier(int value)
	{
		Value = value;
	}

	public bool Equals(PlayerIdentifier other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return Value.Equals(other.Value);
	}

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((PlayerIdentifier) obj);
	}

	public override int GetHashCode()
	{
		return Value.GetHashCode();
	}

	public override string ToString()
		=> $"Player{Value}";

	public static bool operator ==(PlayerIdentifier left, PlayerIdentifier right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(PlayerIdentifier left, PlayerIdentifier right)
	{
		return !Equals(left, right);
	}
}