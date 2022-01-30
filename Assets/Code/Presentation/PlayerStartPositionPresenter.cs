using UniRx;
using UnityEngine;
using Zenject;

public class PlayerStartPositionPresenter : MonoBehaviour
{
	public string HorizontalAxis;
	public string VerticalAxis;
	public string DropBomb;

	public int PlayerId = 1;
	public PlayerColours Colour;

	[Inject]
	public IPlayersCommands PlayersCommands { private get; set; }

	void Start()
	{
		var config = new PlayerConfig(
			transform.position,
			new PlayerInputAxes(HorizontalAxis, VerticalAxis, DropBomb),
			Colour
		);

		Observable.NextFrame()
			.Subscribe(_ => PlayersCommands.NewPlayer(new PlayerIdentifier(PlayerId), config))
			.AddTo(this);
	}
}