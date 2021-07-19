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
		[SerializeField] CanvasGroup canvasGameOver;

		public delegate void HandleCutscene ();
		public static event HandleCutscene OnIntroPlayed;
		public static event HandleCutscene OnOutroPlayed;

		void Start ()
		{
			
		}

		private void OnEnable ()
		{
			GameController.OnGameSetup += PlayIntro;
			GameController.OnGameOver += PlayOutro;
		}

		private void OnDisable ()
		{
			GameController.OnGameSetup -= PlayIntro;
			GameController.OnGameOver -= PlayOutro;
		}

		public void PlayIntro ()
		{
			StartCoroutine (FadeFromBlack ());
		}

		public void PlayOutro ()
		{
			StartCoroutine (FadeToBlack ());
		}

		IEnumerator FadeFromBlack ()
		{
			OnIntroPlayed?.Invoke ();

			canvasHUD.alpha = 0;
			canvasFade.alpha = 1f;
			SetCanvasState (canvasHUD, false);
			SetCanvasState (canvasFade, true);

			while (canvasFade.alpha > 0)
			{
				canvasFade.alpha -= Time.deltaTime / fadeTime;

				yield return null;
			}

			canvasFade.alpha = 0f;
			canvasHUD.alpha = 1f;
			SetCanvasState (canvasFade, false);
			SetCanvasState (canvasHUD, true);

			GameController.Instance.StartGame ();
		}

		IEnumerator FadeToBlack ()
		{
			OnOutroPlayed?.Invoke ();

			canvasHUD.alpha = 0;
			SetCanvasState (canvasHUD, false);
			SetCanvasState (canvasFade, true);

			while (canvasFade.alpha < 1)
			{
				canvasFade.alpha += Time.deltaTime / fadeTime;

				yield return null;
			}

			canvasFade.alpha = 1f;

			canvasGameOver.alpha = 1f;
			SetCanvasState (canvasGameOver, true);
		}

		private void SetCanvasState (CanvasGroup canvas, bool state)
		{
			canvas.interactable = state;
			canvas.blocksRaycasts = state;
		}
	}
}