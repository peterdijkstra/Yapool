using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Yapool.VContainer
{
	public class ResolvingGameObjectProcessor : IProcessor<GameObject>
	{
		private readonly IObjectResolver _resolver;

		public ResolvingGameObjectProcessor(IObjectResolver resolver)
		{
			_resolver = resolver;
		}

		public GameObject CreateObject(GameObject source)
		{
			var instance = Object.Instantiate(source);
			_resolver.InjectGameObject(instance);
			DisableInstance(instance);
			return instance;
		}

		public void DisposeInstance(GameObject instance)
		{
			Object.Destroy(instance);
		}

		public void EnableInstance(GameObject instance)
		{
			instance.SetActive(true);
		}

		public void DisableInstance(GameObject instance)
		{
			instance.SetActive(false);
		}
	}
}