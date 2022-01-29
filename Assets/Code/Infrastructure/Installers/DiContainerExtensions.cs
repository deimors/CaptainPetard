using System;
using System.ComponentModel;
using UniRx;
using UnityEngine;
using Zenject;

public static class DiContainerExtensions
{
	public static IfNotBoundBinder BindIntegration<TIntegration>(this DiContainer container) where TIntegration : IDisposable
		=> container.Bind<IDisposable>().To<TIntegration>().AsSingle().NonLazy();

	public static void BindPrefabFactory<TFactory, TParameters>(this DiContainer container, Transform parent, GameObject prefab)
		where TFactory : IFactory<TParameters, Unit>
	{
		container.BindInstance(parent).WhenInjectedInto<TFactory>();
		container.BindInstance(prefab).WhenInjectedInto<TFactory>();
		container.BindIFactory<TParameters, Unit>().FromFactory<TFactory>();
	}
}