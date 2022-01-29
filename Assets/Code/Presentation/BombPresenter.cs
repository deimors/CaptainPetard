using System;
using UniRx;
using UnityEngine;

public class BombPresenter : MonoBehaviour
{
	public GameObject ExplosionPrefab;
	public double ExplodeTimeSeconds = 1.5;

	void Start()
	{
		Observable.Timer(TimeSpan.FromSeconds(ExplodeTimeSeconds))
			.Subscribe(_ => Explode())
			.AddTo(this);
	}

	private void Explode()
	{
		Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

		Destroy(gameObject);
	}
}
