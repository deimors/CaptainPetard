using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameOverPresenter : MonoBehaviour
{
	public GameObject GameOverPanel;
	
	private bool _gameOver;

	[Inject]
	public IGameEvents GameEvents { private get; set; }

	void Start()
	{
		GameOverPanel.SetActive(false);

		GameEvents
			.OfType<GameEvent, GameEvent.GameOver>()
			.Subscribe(_ =>
			{
				_gameOver = true;
				GameOverPanel.SetActive(true);
			})
			.AddTo(this);
	}

	void Update()
	{
		if (_gameOver && Input.anyKeyDown)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
