public readonly struct PlayerState
{
	public int BombCount { get; }
	public int LifeCount { get; }
	public bool Alive { get; }

	public PlayerState(int bombCount, int lifeCount, bool alive)
	{
		BombCount = bombCount;
		LifeCount = lifeCount;
		Alive = alive;
	}

	public PlayerState UseBomb()
		=> new (
			BombCount - 1,
			LifeCount,
			Alive
		);

	public PlayerState AddBomb()
		=> new(
			BombCount + 1,
			LifeCount,
			Alive
		);

	public PlayerState Kill()
		=> new(
			BombCount,
			Alive ? LifeCount - 1 : LifeCount,
			false
		);

	public PlayerState Revive()
		=> new(
			BombCount,
			LifeCount,
			true
		);
}