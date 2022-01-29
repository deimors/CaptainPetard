using UnityEngine;
using Zenject;

public class PlayerPresenter : MonoBehaviour
{
	public SpriteRenderer SpriteRenderer;
	public Rigidbody2D Rigidbody;
	public float VelocityScale;
	
	[Inject]
	public IPlayersCommands PlayersCommands { private get; set; }
	
	[Inject]
	public PlayerParameters Parameters { private get; set; }

	void Start()
	{
		SpriteRenderer.color = Parameters.Colour.GetColor();
	}

	void Update()
	{
		if (Mathf.Abs(Input.GetAxisRaw(Parameters.InputAxes.DropBomb)) > 0)
		{
			PlayersCommands.DropBomb(Parameters.PlayerId, transform.position);
		}
	}

	void FixedUpdate()
	{
		var inputVector = new Vector2(Input.GetAxis(Parameters.InputAxes.Horizontal), Input.GetAxis(Parameters.InputAxes.Vertical));

		Rigidbody.velocity = inputVector * Time.fixedDeltaTime * VelocityScale;
	}
}
