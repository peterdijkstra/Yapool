namespace Yapool
{
	public interface IInstance<out T>
	{
		T     InstanceObject { get; }
		bool  ActiveSelf     { get; }
		float ActiveTime     { get; }
		void  SetActive(bool value);
	}
}