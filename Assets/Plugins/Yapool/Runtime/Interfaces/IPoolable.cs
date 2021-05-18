using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yapool
{
	public interface IPoolable : ICloneable, IDisposable
	{
		bool Active { get; set; }

		void OnCreate();
	}
}