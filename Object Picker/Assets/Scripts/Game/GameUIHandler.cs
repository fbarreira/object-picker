using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SampleProjects.ObjectPicker
{
	public class GameUIHandler : MonoBehaviour
	{
		[SerializeField] TMP_Text pointsDisplay;
		[SerializeField] TMP_Text objectiveDisplay;
		[SerializeField] GameObject objectiveCheck;

		public void UpdatePointsDisplay (int points)
		{
			pointsDisplay.text = points.ToString ();
		}

		public void UpdateObjectiveCount (int count, int total)
		{
			objectiveDisplay.text = string.Format ("Place All objects on the table ({0}/{1})", count, total);
		}

		public void EnableObjectiveCheck ()
		{
			objectiveCheck.SetActive (true);
		}
	}
}