using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
	public Rigidbody2D Rigidbody;
	public float VelocityScale;

	// Start is called before the first frame update

	void Start()
	{

	}

	// Update is called once per frame

	void FixedUpdate()
	{
		var inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		Rigidbody.velocity = inputVector * Time.fixedDeltaTime * VelocityScale;
	}
}
