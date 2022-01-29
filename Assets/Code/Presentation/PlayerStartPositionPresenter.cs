using UnityEngine;
using Zenject;

public class PlayerStartPositionPresenter : MonoBehaviour
{
	[Inject]
	public IPlayersCommands PlayersCommands { private get; set; }

	void Start()
	{
		PlayersCommands.NewPlayer(transform.position);
	}
}
