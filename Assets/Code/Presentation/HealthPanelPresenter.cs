using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthPanelPresenter : MonoBehaviour
{
	public GameObject HeartPrefab;
	public int PlayerId = 1;

	private GameObject[] _hearts;

	[Inject]
	public IPlayersEvents PlayersEvents { private get; set; }

	void Start()
	{
		PlayersEvents
			.OfType<PlayersEvent, PlayersEvent.PlayerAdded>()
			.Where(playerAdded => playerAdded.PlayerId.Value == PlayerId)
			.Subscribe(playerAdded => CreateHearts(playerAdded.LifeCount))
			.AddTo(this);

		PlayersEvents
			.OfType<PlayersEvent, PlayersEvent.PlayerKilled>()
			.Where(playerKilled => playerKilled.PlayerId.Value == PlayerId)
			.Subscribe(playerKilled => UpdateHearts(playerKilled.RemainingLifeCount))
			.AddTo(this);
	}

	private void CreateHearts(int count)
	{
		_hearts = Enumerable.Range(0, count)
			.Select(_ => Instantiate(HeartPrefab, transform))
			.ToArray();
	}

	private void UpdateHearts(int remainingLifeCount)
	{
		foreach (var index in Enumerable.Range(0, _hearts.Length).Where(index => index > (remainingLifeCount - 1)))
		{
			var heart = _hearts[index];

			heart.GetComponent<Image>().color = Color.black;
		}
	}
}
