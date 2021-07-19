using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SampleProjects.ObjectPicker
{
	[RequireComponent (typeof (CanvasGroup))]
	public class PauseMenuUI : MonoBehaviour
	{
		CanvasGroup canvasGroup;

		bool isPaused;

		private void Awake ()
		{
			canvasGroup = GetComponent<CanvasGroup> ();
		}

		private void OnEnable ()
		{
			InputHandler.OnPauseClicked += UpdateState;
		}

		private void OnDisable ()
		{
			InputHandler.OnPauseClicked -= UpdateState;
		}

		private void UpdateState ()
		{
			isPaused = !isPaused;

			canvasGroup.alpha = isPaused ? 1 : 0;
			canvasGroup.interactable = isPaused;
			canvasGroup.blocksRaycasts = isPaused;

			Time.timeScale = isPaused ? 0 : 1;
		}
	}
}