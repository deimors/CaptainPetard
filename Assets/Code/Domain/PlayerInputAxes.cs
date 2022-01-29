public readonly struct PlayerInputAxes
{
	public string Horizontal { get; }
	public string Vertical { get; }
	public string DropBomb { get; }

	public PlayerInputAxes(string horizontal, string vertical, string dropBomb)
	{
		Horizontal = horizontal;
		Vertical = vertical;
		DropBomb = dropBomb;
	}
}