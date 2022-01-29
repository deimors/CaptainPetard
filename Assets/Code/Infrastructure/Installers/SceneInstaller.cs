using UniRx;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
	public Transform PlayersContainer;
	public GameObject PlayerPrefab;

	public override void InstallBindings()
	{
		Container.BindInterfacesTo<PlayersAggregate>().AsSingle().NonLazy();

		Container.BindInstance(PlayersContainer).WhenInjectedInto<PlayerFactory>();
		Container.BindInstance(PlayerPrefab).WhenInjectedInto<PlayerFactory>();
		Container.BindIFactory<PlayerParameters, Unit>().FromFactory<PlayerFactory>();

		Container.BindIntegration<CreateNewPlayerOnNewPlayerCreated>();
	}
}