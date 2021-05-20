using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Yapool
{
	public abstract class SimplePool<T> : IPool<T>
	{
		public T                         Source    { get; }
		public IEnumerable<IInstance<T>> Instances => instances;
		public IProcessor<T>             Processor { get; }

		public int InstanceCount       => instances.Count;
		public int ActiveInstanceCount { get; private set; }

		protected readonly List<IInstance<T>> instances;

		public SimplePool(T source, int instanceCount, IProcessor<T> processor)
		{
			// Set data
			Source    = source;
			instances = new List<IInstance<T>>(instanceCount);
			Processor = processor;
			
			// Create instances
			for (var i = 0; i < instanceCount; i++)
				instances.Add(CreateInstance());
		}

		protected Instance<T> CreateInstance()
		{
			var obj      = Processor.CreateObject(Source);
			var instance = new Instance<T>(obj);
			
			return instance;
		}

		public T GetObject()
		{
			var value = GetInstance();

			// Enable instance
			value.SetActive(true);
			Processor.EnableInstance(value.InstanceObject);
			ActiveInstanceCount = Instances.Count(x => x.ActiveSelf);
			return value.InstanceObject;
		}

		protected abstract IInstance<T> GetInstance();

		public void ReturnObject(T value)
		{
			// Find instance 
			foreach (var instance in Instances)
			{
				if (!instance.InstanceObject.Equals(value))
					continue;

				// Disable instance
				instance.SetActive(false);
				Processor.DisableInstance(instance.InstanceObject);
				ActiveInstanceCount = Instances.Count(x => x.ActiveSelf);
				return;
			}

			// Instance not tracked
			Debug.LogWarning($"Pool<{Source.GetType()}> does not contain object ({value}).");
		}

		public IInstance<T> GetInstanceAt(int index) => instances[index];

		public void Dispose()
		{
			// Destroy instances
			var instanceCount = instances.Count;
			for (var i = 0; i < instanceCount; i++)
				Processor.DisposeInstance(instances[i].InstanceObject);
			instances.Clear();
		}
	}
}