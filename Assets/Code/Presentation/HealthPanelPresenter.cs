using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

public class HealthPanelPresenter : MonoBehaviour
{
	public GameObject HeartPrefab;
	public int PlayerId = 1;

	[Inject]
	public IPlayersEvents PlayersEvents { private get; set; }

	void Start()
	{
		PlayersEvents
			.OfType<PlayersEvent, PlayersEvent.PlayerAdded>()
			.Where(playerAdded => playerAdded.PlayerId.Value == PlayerId)
			.Subscribe(playerAdded => CreateHearts(playerAdded.LifeCount))
			.AddTo(this);
	}

	private void CreateHearts(int count)
	{
		foreach (var i in Enumerable.Range(0, count))
		{
			Instantiate(HeartPrefab, transform);
		}
	}
}
