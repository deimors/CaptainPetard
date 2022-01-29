public readonly struct PlayerInputAxes
{
	public string Horizontal { get; }
	public string Vertical { get; }

	public PlayerInputAxes(string horizontal, string vertical)
	{
		Horizontal = horizontal;
		Vertical = vertical;
	}
}