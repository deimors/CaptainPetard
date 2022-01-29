using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
	public Rigidbody2D Rigidbody;
	public float VelocityScale;

	public GameObject BombPrefab;

	// Start is called before the first frame update

	void Start()
	{

	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			var bombObject = Instantiate(BombPrefab);

			var bombTransform = bombObject.GetComponent<Transform>();

			bombTransform.position = transform.position;
		}
	}

	void FixedUpdate()
	{
		var inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		Rigidbody.velocity = inputVector * Time.fixedDeltaTime * VelocityScale;
	}
}
