using System;
using UniRx;
using UnityEngine;
using Zenject;

public class BombPresenter : MonoBehaviour
{
	public GameObject ExplosionPrefab;
	public double ExplodeTimeSeconds = 1.5;
	public SpriteRenderer ColourIndicatorSprite;
	public AudioSource explosionSound;
	public LayerMask BombDropperLayers;
	public Collider2D BombCollider;

	[Inject]
	public IPlayersCommands PlayersCommands { private get; set; }

	[Inject]
	public BombParameters Parameters { private set; get; }

	void Start()
	{
		BombCollider.isTrigger = true;
		ColourIndicatorSprite.color = Parameters.PlayerColour.ToColor();

		Observable.Timer(TimeSpan.FromSeconds(ExplodeTimeSeconds))
			.Subscribe(_ => Explode())
			.AddTo(this);
	}

	private void Explode()
	{
		PlayersCommands.ReturnBomb(Parameters.OwnerId);
		PlaySound();

		Instantiate(ExplosionPrefab, transform.position, Quaternion.identity)
			.GetComponent<ExplosionPresenter>()
			.Init(Parameters.PlayerColour);

		Destroy(gameObject);
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if ((BombDropperLayers.value & (1 << other.gameObject.layer)) != 0)
		{
			BombCollider.isTrigger = false;
		}
	}

	private void PlaySound()
	{
		Instantiate(explosionSound).GetComponent<AudioSource>().Play();
	}
	
}