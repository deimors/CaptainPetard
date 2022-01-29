using UnityEngine;
using Zenject;

public class PlayerPresenter : MonoBehaviour
{
	public Rigidbody2D Rigidbody;
	public float VelocityScale;
	
	[Inject]
	public IPlayersCommands PlayersCommands { private get; set; }
	
	[Inject]
	public PlayerParameters Parameters { private get; set; }

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
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
