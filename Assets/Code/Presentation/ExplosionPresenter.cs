using System;
using UniRx;
using UnityEngine;

public class ExplosionPresenter : MonoBehaviour
{
	public int Count = 10;
	public Vector2? Direction = null;
	public int Index = 0;
	public float ChildDistance = 0.5f;
	public GameObject ChildPrefab;
	public int InstantiateDelayMs = 20;

	private static readonly Vector2[] CardinalDirections = { Vector2.up, Vector2.down, Vector2.left, Vector3.right };

	void Start()
	{
		if (Direction is null && Index == 0)
		{
			foreach (var direction in CardinalDirections)
			{
				InstantiateExplosionDelayed(direction);
			}
		}
		else if (Direction is not null && Index < Count)
		{
			InstantiateExplosionDelayed(Direction.Value);
		}
	}

	private void InstantiateExplosionDelayed(Vector2 direction)
		=> Observable
			.Timer(TimeSpan.FromMilliseconds(InstantiateDelayMs))
			.Subscribe(_ => InstantiateExplosion(direction))
			.AddTo(this);

	private void InstantiateExplosion(Vector2 direction)
	{
		var explosion = Instantiate(
			ChildPrefab,
			transform.position + ChildDistance * (Vector3)direction,
			Quaternion.identity
		).GetComponent<ExplosionPresenter>();

		explosion.Direction = direction;
		explosion.Index = Index + 1;
	}
}
