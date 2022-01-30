using UniRx;
using UnityEngine;
using Zenject;

public class EnemySpawnPoint : MonoBehaviour
{
	public PlayerColours Colour;

	[Inject]
	public IEnemiesCommands EnemiesCommands { private get; set; }

	void Start()
	{
		Observable.NextFrame()
			.Subscribe(_ => EnemiesCommands.AddEnemy(transform.position, Colour))
			.AddTo(this);
	}
}