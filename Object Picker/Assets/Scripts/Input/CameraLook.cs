using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SampleProjects.ObjectPicker
{
	public class CameraLook : MonoBehaviour
	{
		[SerializeField] InputHandler input;
		[SerializeField] Transform target;
		[SerializeField] float verticalSensitivity = .5f;
		[SerializeField] float horizontalSensitivity = .5f;
		[SerializeField] float maxLookUpAngle = 70f;
		[SerializeField] float minLookUpAngle = -70f;

		Vector2 mouseInput;
		Vector2 rotation;

		bool isLocked = false;

		void Start ()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		private void OnEnable ()
		{
			InputHandler.OnPauseClicked += HandlePause;
		}

		private void OnDisable ()
		{
			InputHandler.OnPauseClicked -= HandlePause;
		}

		void Update ()
		{
			if (isLocked) return;

			mouseInput = input.GetPlayerLook ();
			rotation.x += mouseInput.x * horizontalSensitivity;
			rotation.y += mouseInput.y * verticalSensitivity;
			rotation.y = Mathf.Clamp (rotation.y, minLookUpAngle, maxLookUpAngle);

			transform.localRotation = Quaternion.Euler (-rotation.y, rotation.x, 0f);
		}

		private void FixedUpdate ()
		{
			transform.position = target.position;
		}

		private void HandlePause ()
		{
			isLocked = !isLocked;
		}
	}
}
