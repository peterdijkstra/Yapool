namespace Yapool
{
	public class FixedSizePool<T> : SimplePool<T>
	{
		public FixedSizePool(T source, int instanceCount, IProcessor<T> processor) : base(source, instanceCount,
			processor)
		{
		}

		protected override IInstance<T> GetInstance()
		{
			// Get instance
			var value = this.GetInactiveInstance();
			if (value == null)
				value = this.GetOldestInstance();
			return value;
		}
	}
}