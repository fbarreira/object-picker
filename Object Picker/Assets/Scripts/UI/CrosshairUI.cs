using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SampleProjects.ObjectPicker
{
	public class CrosshairUI : MonoBehaviour
	{
		[SerializeField] Image dotIcon;
		[SerializeField] Color defaultColor;
		[SerializeField] Color activeColor;

		private void OnEnable ()
		{
			InspectRaycast.OnObjectEnter += SetActive;
			InspectRaycast.OnObjectLeave += SetDefault;
		}

		private void OnDisable ()
		{
			InspectRaycast.OnObjectEnter -= SetActive;
			InspectRaycast.OnObjectLeave -= SetDefault;
		}

		private void SetActive ()
		{
			dotIcon.color = activeColor;
		}

		private void SetDefault ()
		{
			dotIcon.color = defaultColor;
		}
	}
}