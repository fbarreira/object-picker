using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player Instance;

	[SerializeField] Transform itemSlot;

	private void Awake ()
	{
		if (Instance != null)
			Destroy (gameObject);

		Instance = this;
	}

	// Start is called before the first frame update
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	public Transform GetItemSlot () => itemSlot;
}
