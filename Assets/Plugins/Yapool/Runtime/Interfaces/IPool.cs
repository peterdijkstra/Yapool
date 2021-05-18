using System;
using System.Collections.Generic;

namespace Yapool
{
	public interface IPool<T> : IDisposable
	{
		T                         Source    { get; }
		IEnumerable<IInstance<T>> Instances { get; }
		IProcessor<T>             Processor { get; }

		int InstanceCount       { get; }
		int ActiveInstanceCount { get; }

		T            GetObject();
		void         ReturnObject(T    value);
		IInstance<T> GetInstanceAt(int index);
	}
}