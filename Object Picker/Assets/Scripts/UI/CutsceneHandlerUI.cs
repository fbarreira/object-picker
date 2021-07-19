using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SampleProjects.ObjectPicker
{
	public class CutsceneHandlerUI : MonoBehaviour
	{
		[SerializeField] float fadeTime = 1.5f;
		[SerializeField] CanvasGroup canvasHUD;
		[SerializeField] CanvasGroup canvasFade;

		bool isPlaying;

		// Start is called before the first frame update
		void Start ()
		{
			
		}

		// Update is called once per frame
		void Update ()
		{

		}

		public void PlayIntro ()
		{
			isPlaying = true;
		}

		public void PlayOutro ()
		{

		}
	}
}