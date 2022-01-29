using UnityEngine;
using Zenject;

public class PlayerPresenter : MonoBehaviour
{
	public Rigidbody2D Rigidbody;
	public float VelocityScale;

	public GameObject BombPrefab;

	[Inject]
	public IPlayersCommands PlayersCommands { private get; set; }

	[Inject]
	public PlayerIdentifier PlayerId { private get; set; }

	// Start is called before the first frame update

	void Start()
	{

	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			PlayersCommands.DropBomb(PlayerId, transform.position);
		}
	}

	void FixedUpdate()
	{
		var inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		Rigidbody.velocity = inputVector * Time.fixedDeltaTime * VelocityScale;
	}
}
