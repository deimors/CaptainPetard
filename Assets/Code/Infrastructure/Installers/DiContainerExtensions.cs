using System;
using Zenject;

public static class DiContainerExtensions
{
	public static IfNotBoundBinder BindIntegration<TIntegration>(this DiContainer container) where TIntegration : IDisposable
		=> container.Bind<IDisposable>().To<TIntegration>().AsSingle().NonLazy();
}