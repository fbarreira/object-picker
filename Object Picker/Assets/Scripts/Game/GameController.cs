using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SampleProjects.ObjectPicker
{
	public class GameController : MonoBehaviour
	{
		public static GameController Instance;

		[SerializeField] int totalObjective = 3;

		int objectiveCompleted = 0;
		int currentPoints;

		GameUIHandler ui;

		public delegate void HandleGameState ();

		public static event HandleGameState OnGameSetup;
		public static event HandleGameState OnGameStart;
		public static event HandleGameState OnGameOver;

		private void Awake ()
		{
			if (Instance != null)
				Destroy (gameObject);

			Instance = this;

			ui = GetComponent<GameUIHandler> ();
		}

		private void Start ()
		{
			OnGameSetup?.Invoke ();
			ui.UpdatePointsDisplay (currentPoints);
			ui.UpdateObjectiveCount (objectiveCompleted, totalObjective);
		}

		private void OnEnable ()
		{
			Objective.OnOjectiveCompleted += CompleteObjective;
		}

		private void OnDisable ()
		{
			Objective.OnOjectiveCompleted -= CompleteObjective;
		}

		public void StartGame ()
		{
			OnGameStart?.Invoke ();
		}

		private void CompleteObjective (int points)
		{
			objectiveCompleted++;
			currentPoints += points;

			ui.UpdatePointsDisplay (currentPoints);
			ui.UpdateObjectiveCount (objectiveCompleted, totalObjective);

			if (objectiveCompleted == totalObjective)
			{
				Debug.Log ("Game completed.");
				ui.EnableObjectiveCheck ();
				OnGameOver?.Invoke ();

				Cursor.lockState = CursorLockMode.None;
			}
		}


	}
}