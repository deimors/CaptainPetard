using UnityEngine;
using Zenject;

public class PlayerStartPositionPresenter : MonoBehaviour
{
	public string HorizontalAxis;
	public string VerticalAxis;

	public PlayerColours Colour;

	[Inject]
	public IPlayersCommands PlayersCommands { private get; set; }

	void Start()
	{
		PlayersCommands.NewPlayer(
			transform.position, 
			new PlayerInputAxes(HorizontalAxis, VerticalAxis),
			Colour
		);
	}
}