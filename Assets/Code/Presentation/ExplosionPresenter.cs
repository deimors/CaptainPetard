using System;
using System.Linq;
using UniRx;
using UnityEngine;

public class ExplosionPresenter : MonoBehaviour
{
	public int Count = 10;
	public float ChildDistance = 0.5f;
	public GameObject ChildPrefab;
	public int InstantiateDelayMs = 20;
	public float TriggerRadius = 0.2f;
	public LayerMask StopLayers;
	public SpriteRenderer ExplosionRenderer;

	private static readonly Vector2[] CardinalDirections = { Vector2.up, Vector2.down, Vector2.left, Vector3.right };

	private PlayerColours _playerColour;
	private Vector2? _direction = null;
	private int _index = 0;

	void Start()
	{
		var colliders = GetOverlappingColliders();

		SendHandleExplosionToColliders(colliders);

		if (IsOverlappingStopLayer(colliders))
			return;

		if (_direction is null && _index == 0)
		{
			foreach (var direction in CardinalDirections)
			{
				InstantiateExplosionDelayed(direction);
			}
		}
		else if (_direction is not null && _index < Count)
		{
			InstantiateExplosionDelayed(_direction.Value);
		}
	}

	public void Init(PlayerColours playerColour)
	{
		_playerColour = playerColour;
		ExplosionRenderer.color = playerColour.ToColor();
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

		explosion._direction = direction;
		explosion._index = _index + 1;
		explosion._playerColour = _playerColour;
	}

	private bool IsOverlappingStopLayer(Collider2D[] colliders)
		=> colliders.Any(c => (StopLayers.value & (1 << c.gameObject.layer)) != 0);

	private void SendHandleExplosionToColliders(Collider2D[] colliders)
	{
		foreach (var otherCollider in colliders)
		{
			otherCollider.SendMessage("HandleExplosion", _playerColour, SendMessageOptions.DontRequireReceiver);
		}
	}

	private Collider2D[] GetOverlappingColliders()
		=> Physics2D.OverlapCircleAll(transform.position, TriggerRadius);
}
