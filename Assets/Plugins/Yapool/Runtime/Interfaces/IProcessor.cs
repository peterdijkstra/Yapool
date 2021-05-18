namespace Yapool
{
	public interface IProcessor<T>
	{
		/// <summary>Create new Instance of Type T using source object.</summary> 
		/// <param name="key">Unique identifer for Pool.</param>
		/// <param name="source">Source object.</param>
		/// <returns>New instance.</returns> 
		public T CreateObject(T source);

		/// <summary>Destroy Instance of Type T.</summary> 
		/// <param name="key">Unique identifer for Pool.</param>
		/// <param name="instance">Instance to destroy.</param>
		public void DisposeInstance(T instance);

		/// <summary>Called when instance is enabled.</summary> 
		/// <param name="key">Unique identifer for Pool.</param>
		/// <param name="instance">Instance to enable.</param>
		public void EnableInstance(T instance);

		/// <summary>Called when instance is disabled.</summary> 
		/// <param name="key">Unique identifer for Pool.</param>
		/// <param name="instance">Instance to disable.</param>
		public void DisableInstance(T instance);
	}
}