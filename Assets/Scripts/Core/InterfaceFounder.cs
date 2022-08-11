using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Core
{
	/// <summary>
	/// Static class for finding different interfaces in <see cref="Scene"/>
	/// </summary>
	public static class InterfaceFounder
	{
		public static List<T> FindInterface<T>()
		{
			var type = typeof(T);
			if (!type.IsInterface)
			{
				throw new ArgumentException($"Type ${type} is not interface!");
			}

			var interfaces = new List<T>();
			var rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
			foreach (var rootGameObject in rootGameObjects)
			{
				var childrenInterfaces = rootGameObject.GetComponentsInChildren<T>();
				interfaces.AddRange(childrenInterfaces);
			}

			return interfaces;
        }
	}
}
