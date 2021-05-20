using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using Yapool;

public static class ContainerBuilderExtensions
{
	/// <summary>
	/// Only takes instanceCount instance
	/// </summary>
	/// <param name="builder"></param>
	/// <param name="instanceCount"></param>
	/// <param name="lifetime"></param>
	/// <typeparam name="TPool"></typeparam>
	/// <typeparam name="TSource"></typeparam>
	/// <typeparam name="TProcessor"></typeparam>
	public static void RegisterPool<TPool, TSource, TProcessor>(this IContainerBuilder builder,
		int instanceCount, Lifetime lifetime = Lifetime.Scoped) where TPool : class, IPool<TSource>
	{
		builder.Register(resolver =>
		{
			var source    = resolver.Resolve<TSource>();
			var processor = resolver.Resolve<TProcessor>();

			var pool      = Activator.CreateInstance(typeof(TPool), source, instanceCount, processor);
			
			Debug.Log(pool);
			
			return pool as TPool;
		}, lifetime);
	}

	/// <summary>
	/// Takes TSource and instanceCount instances
	/// </summary>
	/// <param name="builder"></param>
	/// <param name="source"></param>
	/// <param name="instanceCount"></param>
	/// <param name="lifetime"></param>
	/// <typeparam name="TPool"></typeparam>
	/// <typeparam name="TSource"></typeparam>
	/// <typeparam name="TProcessor"></typeparam>
	public static void RegisterPool<TPool, TSource, TProcessor>(this IContainerBuilder builder, TSource source,
		int instanceCount, Lifetime lifetime = Lifetime.Scoped) where TPool : class, IPool<TSource>
	{
		builder.Register(resolver =>
		{
			var processor = resolver.Resolve<TProcessor>();
			var pool      = Activator.CreateInstance(typeof(TPool), source, instanceCount, processor);
			return pool as TPool;
		}, lifetime);
	}

	/// <summary>
	/// Takes TSource, instanceCount and TProcessor instances
	/// </summary>
	/// <param name="builder"></param>
	/// <param name="source"></param>
	/// <param name="instanceCount"></param>
	/// <param name="processor"></param>
	/// <param name="lifetime"></param>
	/// <typeparam name="TPool"></typeparam>
	/// <typeparam name="TSource"></typeparam>
	/// <typeparam name="TProcessor"></typeparam>
	public static void RegisterPool<TPool, TSource, TProcessor>(this IContainerBuilder builder, TSource source,
		int instanceCount, TProcessor processor, Lifetime lifetime = Lifetime.Scoped)
		where TPool : class, IPool<TSource>
	{
		builder.Register(_ =>
		{
			var pool = Activator.CreateInstance(typeof(TPool), source, instanceCount, processor);
			return pool as TPool;
		}, lifetime);
	}

	/// <summary>
	/// Takes TPool instance
	/// </summary>
	/// <param name="builder"></param>
	/// <param name="pool"></param>
	/// <typeparam name="TPool"></typeparam>
	/// <typeparam name="TSource"></typeparam>
	public static void RegisterPool<TPool, TSource>(this IContainerBuilder builder, TPool pool)
		where TPool : class, IPool<TSource>
	{
		builder.RegisterInstance<TPool>(pool);
	}
}