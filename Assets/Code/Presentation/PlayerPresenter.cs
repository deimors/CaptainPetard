using UniRx;
using UnityEngine;
using Zenject;

public class PlayerPresenter : MonoBehaviour
{
	public SpriteRenderer SpriteRenderer;
	public Rigidbody2D Rigidbody;
	public float VelocityScale;

	private bool _bombButtonLatch;
	
	[Inject]
	public IPlayersCommands PlayersCommands { private get; set; }

	[Inject]
	public IPlayersEvents PlayersEvents { private get; set; }
	
	[Inject]
	public PlayerParameters Parameters { private get; set; }

	void Start()
	{
		SpriteRenderer.color = Parameters.Colour.GetColor();

		PlayersEvents
			.OfType<PlayersEvent, PlayersEvent.PlayerKilled>()
			.Where(playerKilled => playerKilled.PlayerId == Parameters.PlayerId)
			.Subscribe(_ => Destroy(gameObject))
			.AddTo(this);
	}

	void Update()
	{
		if (Mathf.Abs(Input.GetAxisRaw(Parameters.InputAxes.DropBomb)) > 0)
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
		var inputVector = new Vector2(Input.GetAxis(Parameters.InputAxes.Horizontal), Input.GetAxis(Parameters.InputAxes.Vertical));

		Rigidbody.velocity = inputVector * Time.fixedDeltaTime * VelocityScale;
	}

	public void HandleExplosion()
	{
		PlayersCommands.KillPlayer(Parameters.PlayerId);
	}
}
