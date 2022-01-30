using UniRx;
using UnityEngine;
using Zenject;

public class PlayerPresenter : MonoBehaviour
{
	public SpriteRenderer SpriteRenderer;
	public Rigidbody2D Rigidbody;
	public float VelocityScale;
	public LayerMask KillOnContact;
	public AudioSource deathSound;

	private bool _bombButtonLatch;
	
	[Inject]
	public IPlayersCommands PlayersCommands { private get; set; }

	[Inject]
	public IPlayersEvents PlayersEvents { private get; set; }
	
	[Inject]
	public PlayerParameters Parameters { private get; set; }

	void Start()
	{
		SpriteRenderer.color = Parameters.Config.Colour.ToColor();

		PlayersEvents
			.OfType<PlayersEvent, PlayersEvent.PlayerKilled>()
			.Where(playerKilled => playerKilled.PlayerId == Parameters.PlayerId)
			.Subscribe(_ => Destroy(gameObject))
			.AddTo(this);
	}

	void Update()
	{
		if (Mathf.Abs(Input.GetAxisRaw(Parameters.Config.InputAxes.DropBomb)) > 0)
		{
			if (!_bombButtonLatch)
			{
				_bombButtonLatch = true;
				PlayersCommands.DropBomb(Parameters.PlayerId, transform.position);
			}
		}
		else
		{
			_bombButtonLatch = false;
		}
	}

	void FixedUpdate()
	{
		var inputVector = new Vector2(Input.GetAxis(Parameters.Config.InputAxes.Horizontal), Input.GetAxis(Parameters.Config.InputAxes.Vertical));

		Rigidbody.velocity = inputVector * Time.fixedDeltaTime * VelocityScale;
	}

	public void HandleExplosion(PlayerColours bombPlayerColour)
	{
		if (bombPlayerColour == Parameters.Config.Colour)
		{
			PlaySound(deathSound);
			PlayersCommands.KillPlayer(Parameters.PlayerId);
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if ((KillOnContact.value & (1 << collision.gameObject.layer)) != 0)
		{
			PlaySound(deathSound);
			PlayersCommands.KillPlayer(Parameters.PlayerId);
		}
	}
	
	private void PlaySound(AudioSource source)
	{
		Instantiate(source).GetComponent<AudioSource>().Play();
	}
}
