using UniRx;
using UnityEngine;
using Zenject;

public class GameOverPresenter : MonoBehaviour
{
	public GameObject GameOverPanel;

	[Inject]
	public IGameEvents GameEvents { private get; set; }

	void Start()
	{
		GameOverPanel.SetActive(false);

		GameEvents
			.OfType<GameEvent, GameEvent.GameOver>()
			.Subscribe(_ => GameOverPanel.SetActive(true))
			.AddTo(this);
	}
}
