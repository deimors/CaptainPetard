using System;
using UniRx;
using UnityEngine;
using Zenject;

public class EnemySpawnPoint : MonoBehaviour
{
	public PlayerColours Colour;
	public float RespawnDelaySeconds = 2;

	[Inject]
	public IEnemiesCommands EnemiesCommands { private get; set; }

	[Inject]
	public IEnemiesEvents EnemiesEvents { private get; set; }

	void Start()
	{
		Observable.NextFrame()
			.Subscribe(_ => Spawn())
			.AddTo(this);

		EnemiesEvents
			.OfType<EnemiesEvent, EnemiesEvent.AllEnemiesKilled>()
			.Delay(TimeSpan.FromSeconds(RespawnDelaySeconds))
			.Subscribe(_ => Spawn())
			.AddTo(this);
	}

	private void Spawn()
		=> EnemiesCommands.AddEnemy(transform.position, Colour);
}