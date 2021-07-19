using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SampleProjects.ObjectPicker
{
	[RequireComponent (typeof (Button))]
	public class ButtonLoadScene : MonoBehaviour
	{
		[SerializeField] string sceneName;

		Button button;

		private void Awake ()
		{
			button = GetComponent<Button> ();
			button.onClick.AddListener (LoadScene);
		}

		private void LoadScene ()
		{
			if (string.IsNullOrEmpty (sceneName)) return;

			SceneManager.LoadScene (sceneName);
		}
	}
}