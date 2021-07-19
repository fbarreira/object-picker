using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SampleProjects.ObjectPicker
{
	public class Objective : InteractableObject
	{
		[SerializeField] int points;
		[SerializeField] Transform target;

		bool isHolding;
		bool onObjectArea;
		bool colected;

		Rigidbody _rigidbody;

		public delegate void HandleObjectiveEvent (int p);

		public static event HandleObjectiveEvent OnOjectiveCompleted;

		private void Awake ()
		{
			_rigidbody = GetComponent<Rigidbody> ();
		}

		private void Update ()
		{
			if (isHolding)
			{
				transform.position = Player.Instance.GetItemSlot ().position;
			}
		}

		public override void DoAction ()
		{
			if (isHolding || colected) return;

			isHolding = true;
			_rigidbody.isKinematic = true;

			base.DoAction ();
		}

		public override void Release ()
		{
			if (colected) return;

			if (onObjectArea)
			{
				colected = true;
				transform.position = target.position;
				isInteractable = false;
				OnOjectiveCompleted?.Invoke (points);
			}

			_rigidbody.isKinematic = false;
			isHolding = false;
		}

		private void OnTriggerEnter (Collider other)
		{
			if (other.CompareTag ("ObjectiveArea"))
			{
				onObjectArea = true;
			}
		}

		private void OnTriggerExit (Collider other)
		{
			if (other.CompareTag ("ObjectiveArea"))
			{
				onObjectArea = false;
			}
		}
	}
}