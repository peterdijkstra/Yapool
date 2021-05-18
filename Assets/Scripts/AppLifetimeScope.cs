using UnityEngine;
using VContainer;
using VContainer.Unity;
using Yapool;

public class AppLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterPool<FixedSizePool<GameObject>, GameObject, GameObjectProcessor>(new GameObject(), 5);
    }
}
