using Yapool;

public class MockPoolable : IPoolable<MockPoolable>
{
	public bool Active { get; set; }

	public void OnCreate()
	{
	}

	public MockPoolable Clone()
	{
		var clone = new MockPoolable();
		return clone;
	}

	public void Dispose()
	{
	}
}

public class DependantMockPoolable : IPoolable<DependantMockPoolable>
{
	public SomeService Service { get; }

	public bool Active { get; set; }
	
	public DependantMockPoolable(SomeService someService)
	{
		Service = someService;
	}
	
	public void OnCreate()
	{
	}

	public DependantMockPoolable Clone()
	{
		return new DependantMockPoolable(Service);
	}

	public void Dispose()
	{
	}
}

public class SomeService
{
}