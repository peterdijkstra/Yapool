using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yapool
{
	public class GameObjectProcessor : IProcessor<GameObject>
	{
		public GameObject CreateObject(GameObject source)
		{
			var instance = Object.Instantiate(source);
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