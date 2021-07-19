using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SampleProjects.ObjectPicker
{
	[RequireComponent (typeof (Button))]
	public class ButtonQuit : MonoBehaviour
	{

		Button button;

		private void Awake ()
		{
			button = GetComponent<Button> ();
			button.onClick.AddListener (Quit);
		}

		private void Quit ()
		{
			Debug.Log ("Quitting...");
			Application.Quit ();
		}
	}
}