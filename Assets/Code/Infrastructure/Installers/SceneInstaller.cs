using UniRx;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
	public Transform PlayersContainer;
	public GameObject PlayerPrefab;

	public Transform BombsContainer;
	public GameObject BombPrefab;

	public override void InstallBindings()
	{
		Container.BindInterfacesTo<PlayersAggregate>().AsSingle().NonLazy();

		Container.BindPrefabFactory<PlayerFactory, PlayerParameters>(PlayersContainer, PlayerPrefab);
		Container.BindPrefabFactory<BombFactory, BombParameters>(BombsContainer, BombPrefab);
		
		Container.BindIntegration<CreateNewPlayerOnNewPlayerCreated>();
		Container.BindIntegration<CreateBombOnBombDropped>();
	}
}