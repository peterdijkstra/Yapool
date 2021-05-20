using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yapool
{
	public interface IPoolable<out T> : IDisposable
	{
		bool Active { get; set; }

		void OnCreate();

		T Clone();
	}
}