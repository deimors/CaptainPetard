using System;
using UniRx;
using UnityEngine;
using Zenject;

public class BombPresenter : MonoBehaviour
{
	public GameObject ExplosionPrefab;
	public double ExplodeTimeSeconds = 1.5;

	[Inject]
	public IPlayersCommands PlayersCommands { private get; set; }

	[Inject]
	public BombParameters Parameters { private set; get; }

	void Start()
	{
		Observable.Timer(TimeSpan.FromSeconds(ExplodeTimeSeconds))
			.Subscribe(_ => Explode())
			.AddTo(this);
	}

	private void Explode()
	{
		PlayersCommands.ReturnBomb(Parameters.OwnerId);

		Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

		Destroy(gameObject);
	}
}