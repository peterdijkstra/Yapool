using UnityEngine;

namespace Yapool
{
	public class Instance<T> : IInstance<T>
	{
		public Instance(T instanceObject)
		{
			InstanceObject = instanceObject;
			SetActive(false);
		}
	
		public T InstanceObject { get; }

		public bool ActiveSelf { get; private set; }

		public float ActiveTime { get; private set; }
	
		public void SetActive(bool value)
		{
			ActiveSelf = value;
			ActiveTime = value ? Time.realtimeSinceStartup : 0.0f;
		}
	}
}