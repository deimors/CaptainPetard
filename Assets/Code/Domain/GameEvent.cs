public abstract class GameEvent
{
	public class GameOver : GameEvent
	{
	}

	public class ScoreChanged : GameEvent
	{
		public int Score { get; }

		public ScoreChanged(int score)
		{
			Score = score;
		}
	}
}