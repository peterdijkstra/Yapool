namespace Yapool
{
	public class GrowPool<T> : SimplePool<T>
	{
		public GrowPool(T source, int instanceCount, IProcessor<T> processor) : base(source, instanceCount, processor)
		{
		}

		protected override IInstance<T> GetInstance()
		{
			// Get instance
			var value = this.GetInactiveInstance();
			if (value == null)
				value = this.GetOldestInstance();
			if (value.ActiveSelf)
				value = CreateInstance();

			return value;
		}
	}
}