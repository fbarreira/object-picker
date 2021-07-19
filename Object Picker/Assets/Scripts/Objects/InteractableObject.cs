using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
	[SerializeField] protected string info;

	protected bool isInteractable = true;

	public bool IsInteractable => isInteractable;

	public string Info => info;

	public virtual void DoAction ()
	{
		Debug.Log (name + " interacted.");
	}

	public virtual void DoLongAction ()
	{
		Debug.Log (name + " long action.");
	}

	public virtual void Hold ()
	{
		Debug.Log (name + " holded.");
	}

	public virtual void Release ()
	{
		Debug.Log (name + " Released.");
	}
}
