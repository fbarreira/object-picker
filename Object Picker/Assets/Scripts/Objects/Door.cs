using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SampleProjects.ObjectPicker
{
	public class Door : InteractableObject
	{
		public bool isOpen;

		private bool isMoving;

		[SerializeField] Animator animator;

		private void Start ()
		{
			info = (isOpen) ? "Close" : "Open";
		}

		public override void DoAction ()
		{
			if (isMoving) return;

			StartCoroutine (PlayAnim ());
		}

		private void Open ()
		{
			isOpen = true;
			animator.Play ("Open");
		}

		private void Close ()
		{
			isOpen = false;
			animator.Play ("Close");
		}

		IEnumerator PlayAnim ()
		{
			isOpen = !isOpen;
			isMoving = true;

			string anim = (isOpen) ? "Open" : "Close";
			info = (isOpen) ? "Close" : "Open";

			animator.Play (anim);

			yield return new WaitForSeconds (.5f);

			isMoving = false;
		}

	}
}