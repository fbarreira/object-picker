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

		private void Awake ()
		{
			if (Instance != null)
				Destroy (gameObject);

			Instance = this;

			ui = GetComponent<GameUIHandler> ();
		}

		void Start ()
		{

		}

		void Update ()
		{

		}

		private void OnEnable ()
		{
			Objective.OnOjectiveCompleted += CompleteObjective;
		}

		private void OnDisable ()
		{
			Objective.OnOjectiveCompleted -= CompleteObjective;
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
			}
		}


	}
}