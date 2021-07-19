using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SampleProjects.ObjectPicker
{
	public class PlayerMovement : MonoBehaviour
	{
		[SerializeField] InputHandler input;
		[SerializeField] Transform playerCamera;
		[SerializeField] float moveSpeed = 50f;

		Vector2 inputMovement;
		Vector3 forwardDirection;
		Vector3 rightDiretion;

		void Update ()
		{
			//set y rotation = camera y rotation
			transform.rotation = Quaternion.Euler (new Vector3 (0, playerCamera.eulerAngles.y, 0));

			Movement ();
		}

		private void Movement ()
		{
			inputMovement = input.GetPlayerMovement ();

			forwardDirection = inputMovement.y * moveSpeed * Time.deltaTime * Vector3.forward;
			rightDiretion = inputMovement.x * moveSpeed * Time.deltaTime * Vector3.right;

			transform.Translate (forwardDirection + rightDiretion);
		}
	}
}