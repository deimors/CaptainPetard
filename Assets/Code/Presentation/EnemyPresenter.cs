using System;
using System.Linq;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyPresenter : MonoBehaviour
{
	public float Speed = 100f;
	public Rigidbody2D Rigidbody;
	public LayerMask Obstacles;
	public float InitialDelaySeconds = 1;

	private Vector2 _direction;

	private static readonly Vector2[] CardinalDirections = { Vector2.up, Vector2.down, Vector2.left, Vector3.right };

	void Start()
	{
		Observable.Timer(TimeSpan.FromSeconds(InitialDelaySeconds))
			.Subscribe(_ => ChooseRandomOpenDirection())
			.AddTo(this);
	}

	void FixedUpdate()
	{
		Rigidbody.velocity = _direction * Speed * Time.fixedDeltaTime;
	}

	private void ChooseRandomOpenDirection()
	{
		transform.position = new Vector3(
			Mathf.Round(transform.position.x - 0.5f) + 0.5f,
			Mathf.Round(transform.position.y - 0.5f) + 0.5f,
			transform.position.z
		);

		var openDirections = FindOpenDirections();

		_direction = RandomDirection(openDirections.Where(dir => dir != _direction).ToArray());
	}

	private static Vector2 RandomDirection(Vector2[] directions)
		=> directions.Length == 0 
			? Vector2.zero 
			: directions[Random.Range(0, directions.Length)];

	private Vector2[] FindOpenDirections()
		=> CardinalDirections
			.Where(dir => Physics2D.OverlapCircle(transform.position + (Vector3)dir, 0.45f, Obstacles.value) == null)
			.ToArray();

	void OnCollisionEnter2D(Collision2D collision)
	{
		ChooseRandomOpenDirection();
	}
}