using Zenject;

public class SceneInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.BindInterfacesTo<PlayersAggregate>().AsSingle().NonLazy();
	}
}
