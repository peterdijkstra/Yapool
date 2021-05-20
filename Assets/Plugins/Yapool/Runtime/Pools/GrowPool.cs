namespace Yapool
{
	public class GrowPool<T> : SimplePool<T>
	{
		private readonly int _maxInstanceCount;

		public GrowPool(T source, int instanceCount, IProcessor<T> processor, int maxInstanceCount = int.MaxValue) :
			base(source, instanceCount, processor)
		{
			_maxInstanceCount = maxInstanceCount;
		}

		protected override IInstance<T> GetInstance()
		{
			// Get instance
			var value = this.GetInactiveInstance();
			if (value == null)
				value = this.GetOldestInstance();
			if (value.ActiveSelf && InstanceCount < _maxInstanceCount)
			{
				value = CreateInstance();
				instances.Add(value);
			}

			return value;
		}
	}
}