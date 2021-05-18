using UnityEngine;

namespace Yapool
{
	public static class PoolExtensions
	{
		public static IInstance<T> GetInactiveInstance<T>(this IPool<T> pool)
		{
			foreach (var instance in pool.Instances)
			{
				if (!instance.ActiveSelf)
					return instance;
			}

			return null;
		}

		public static IInstance<T> GetOldestInstance<T>(this IPool<T> pool)
		{
			var          oldestTime     = Mathf.Infinity;
			IInstance<T> oldestInstance = null;
			for (var i = 0; i < pool.InstanceCount; i++)
			{
				var instance = pool.GetInstanceAt(i);
				if (instance.ActiveTime > oldestTime)
					continue;
				oldestTime     = instance.ActiveTime;
				oldestInstance = instance;
			}

			return oldestInstance;
		}
	}
}