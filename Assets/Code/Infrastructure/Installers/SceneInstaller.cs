using UniRx;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
	public Transform PlayersContainer;
	public GameObject PlayerPrefab;

	public Transform BombsContainer;
	public GameObject BombPrefab;

	public Transform EnemiesContainer;
	public GameObject EnemyPrefab;

	public override void InstallBindings()
	{
		Container.BindInterfacesTo<PlayersAggregate>().AsSingle().NonLazy();
		Container.BindInterfacesTo<EnemiesAggregate>().AsSingle().NonLazy();

		Container.BindPrefabFactory<PlayerFactory, PlayerParameters>(PlayersContainer, PlayerPrefab);
		Container.BindPrefabFactory<BombFactory, BombParameters>(BombsContainer, BombPrefab);
		Container.BindPrefabFactory<EnemyFactory, EnemyParameters>(EnemiesContainer, EnemyPrefab);
		
		Container.BindIntegration<CreateNewPlayerOnPlayerSpawned>();
		Container.BindIntegration<CreateBombOnBombDropped>();
		Container.BindIntegration<CreateNewEnemyOnEnemySpawned>();
	}
}