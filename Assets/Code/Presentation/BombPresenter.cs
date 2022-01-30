using System;
using UniRx;
using UnityEngine;
using Zenject;

public class BombPresenter : MonoBehaviour
{
	public GameObject ExplosionPrefab;
	public double ExplodeTimeSeconds = 1.5;
	public SpriteRenderer ColourIndicatorSprite;

	[Inject]
	public IPlayersCommands PlayersCommands { private get; set; }

	[Inject]
	public BombParameters Parameters { private set; get; }

	void Start()
	{
		ColourIndicatorSprite.color = Parameters.PlayerColour.ToColor();

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