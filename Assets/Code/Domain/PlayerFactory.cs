using UniRx;
using UnityEngine;
using Zenject;

public class PlayerFactory : IFactory<PlayerParameters, Unit>
{
	private readonly DiContainer _container;
	private readonly GameObject _prefab;
	private readonly Transform _parent;

	public PlayerFactory(DiContainer container, GameObject prefab, Transform parent)
	{
		_container = container;
		_prefab = prefab;
		_parent = parent;
	}

	public Unit Create(PlayerParameters param)
	{
		var subContainer = _container.CreateSubContainer();

		subContainer.BindInstance(param);

		subContainer.InstantiatePrefab(_prefab, param.Config.Position, Quaternion.identity, _parent);

		return Unit.Default;
	}
}