using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using VContainer;
using VContainer.Unity;
using Yapool;

public class YapoolTests
{
	[Test]
	public void CreateGameObjectPool()
	{
		// arrange
		var       source        = GameObject.CreatePrimitive(PrimitiveType.Cube);
		const int instanceCount = 5;
		var       processor     = new GameObjectProcessor();

		// act
		var pool = new FixedSizePool<GameObject>(source, instanceCount, processor);

		// assert
		Assert.That(pool.Source        == source);
		Assert.That(pool.InstanceCount == 5);
		Assert.That(pool.Processor     == processor);
		foreach (var instance in pool.Instances)
			Assert.That(instance.InstanceObject.activeSelf == false);
	}

	[Test]
	public void GetOneGameObject()
	{
		// arrange
		var       source        = GameObject.CreatePrimitive(PrimitiveType.Cube);
		const int instanceCount = 5;
		var       processor     = new GameObjectProcessor();
		var       pool          = new FixedSizePool<GameObject>(source, instanceCount, processor);

		// act
		var instance = pool.GetObject();

		// assert
		Assert.That(instance                 != source);
		Assert.That(pool.ActiveInstanceCount == 1);
	}

	public class MockPoolable : IPoolable
	{
		public bool Active { get; set; }

		public void OnCreate()
		{
		}

		public object Clone()
		{
			var clone = new MockPoolable();
			return clone;
		}

		public void Dispose()
		{
		}
	}

	[Test]
	public void CreateMockPoolablePool()
	{
		// arrange
		var       source        = new MockPoolable();
		const int instanceCount = 5;
		var       processor     = new PoolableProcessor<MockPoolable>();

		// act
		var pool = new FixedSizePool<MockPoolable>(source, instanceCount, processor);

		// assert
		Assert.That(pool.Source        == source);
		Assert.That(pool.InstanceCount == 5);
		Assert.That(pool.Processor     == processor);
		foreach (var instance in pool.Instances)
			Assert.That(instance.InstanceObject.Active == false);
	}

	[Test]
	public void VContainerTest0()
	{
		var lifeTimeScope = LifetimeScope.Create(builder =>
		{
			builder.RegisterPool<FixedSizePool<GameObject>, GameObject, GameObjectProcessor>(new GameObject(), 5,
				new GameObjectProcessor());
		});

		var pool = lifeTimeScope.Container.Resolve(typeof(FixedSizePool<GameObject>));
		Assert.That(pool != null);
		Assert.That(pool is FixedSizePool<GameObject>);
	}

	[Test]
	public void VContainerTest1()
	{
		var lifeTimeScope = LifetimeScope.Create(builder =>
		{
			builder.Register<GameObjectProcessor>(Lifetime.Scoped);
			builder.RegisterPool<FixedSizePool<GameObject>, GameObject, GameObjectProcessor>(new GameObject(), 5);
		});

		var pool = lifeTimeScope.Container.Resolve(typeof(FixedSizePool<GameObject>)) as FixedSizePool<GameObject>;

		Assert.That(pool != null);

		Assert.That(pool.Processor != null);
		Assert.That(pool.Processor is GameObjectProcessor);
	}

	[Test]
	public void VContainerTest2()
	{
		var source = new GameObject();
		var lifeTimeScope = LifetimeScope.Create(builder =>
		{
			builder.RegisterInstance<GameObject>(source);
			builder.Register<GameObjectProcessor>(Lifetime.Scoped);
			builder.RegisterPool<FixedSizePool<GameObject>, GameObject, GameObjectProcessor>(5);
		});

		var pool = lifeTimeScope.Container.Resolve(typeof(FixedSizePool<GameObject>)) as FixedSizePool<GameObject>;

		Assert.That(pool != null);

		Assert.That(pool.Source == source);

		Assert.That(pool.Processor != null);
		Assert.That(pool.Processor is GameObjectProcessor);
	}

	[Test]
	public void VContainerTest3()
	{
		var source       = new GameObject();
		var processor    = new GameObjectProcessor();
		var poolInstance = new FixedSizePool<GameObject>(source, 5, processor);
		var lifeTimeScope = LifetimeScope.Create(builder =>
		{
			builder.RegisterPool<FixedSizePool<GameObject>, GameObject>(poolInstance);
		});

		var pool = lifeTimeScope.Container.Resolve(typeof(FixedSizePool<GameObject>)) as FixedSizePool<GameObject>;

		Assert.That(pool != null);
		Assert.That(pool == poolInstance);

		Assert.That(pool.Source == source);

		Assert.That(pool.Processor == processor);
		Assert.That(pool.Processor is GameObjectProcessor);
	}

	// A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
	// `yield return null;` to skip a frame.
	// [UnityTest]
	// public IEnumerator YapoolTestsWithEnumeratorPasses()
	// {
	//     // Use the Assert class to test conditions.
	//     // Use yield to skip a frame.
	//     yield return null;
	// }
}