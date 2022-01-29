using UnityEngine;
using Zenject;

public class PlayerStartPositionPresenter : MonoBehaviour
{
	[Inject]
	public IPlayerCommands PlayerCommands { private get; set; }

	void Start()
	{
		PlayerCommands.NewPlayer(transform.position);
	}
}
