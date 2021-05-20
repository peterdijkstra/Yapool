namespace Yapool
{
	public class 
		PoolableProcessor<T> : IProcessor<T> where T : class, IPoolable<T>
	{
		public T CreateObject(T source)
		{
			var clone = source.Clone();
			DisableInstance(clone);
			return clone;
		}

		public void DisposeInstance(T instance)
		{
			instance.Dispose();
		}

		public void EnableInstance(T instance)
		{
			instance.Active = true;
		}

		public void DisableInstance(T instance)
		{
			instance.Active = false;
		}
	}
}