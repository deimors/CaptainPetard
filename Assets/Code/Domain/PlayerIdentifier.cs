using System;

public class PlayerIdentifier : IEquatable<PlayerIdentifier>
{
	private readonly Guid _value;

	private PlayerIdentifier(Guid value)
	{
		_value = value;
	}

	public static PlayerIdentifier Create()
		=> new(Guid.NewGuid());

	public bool Equals(PlayerIdentifier other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return _value.Equals(other._value);
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
		return _value.GetHashCode();
	}

	public static bool operator ==(PlayerIdentifier left, PlayerIdentifier right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(PlayerIdentifier left, PlayerIdentifier right)
	{
		return !Equals(left, right);
	}
}