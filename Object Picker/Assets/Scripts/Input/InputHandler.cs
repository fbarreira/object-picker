using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SampleProjects.ObjectPicker
{
	public class InputHandler : MonoBehaviour
	{
		[SerializeField] bool controlEnabled = true;
		[SerializeField] float slowClickTime = .5f;
		[SerializeField] float holdTime = 1f;

		InputMaster inputActions;

		public InputMaster Actions => inputActions;

		float pressTime = 0;
		bool isMousePressed;
		bool isMouseClicked;
		bool isMouseSlowClicked;
		bool isMouseHolding;

		public static InputHandler Instance;

		public delegate void HandleInput ();

		public static event HandleInput OnPauseClicked;

		private void Awake ()
		{
			if (Instance != null)
				Destroy (gameObject);

			Instance = this;

			inputActions = new InputMaster ();
		}

		private void OnEnable ()
		{
			inputActions.Enable ();
			inputActions.Player.Interact.started += Interact_started;
			inputActions.Player.Interact.performed += Interact_performed;
			inputActions.Player.Interact.canceled += Interact_canceled;
			inputActions.Player.Pause.performed += Pause_performed;
		}

		private void OnDisable ()
		{
			inputActions.Disable ();
			inputActions.Player.Interact.started -= Interact_started;
		}

		private void Update ()
		{
			if (isMousePressed)
			{
				pressTime += Time.deltaTime;
			}
		}

		public Vector2 GetPlayerMovement () => Actions.Player.Movement.ReadValue<Vector2> ();

		public Vector2 GetPlayerLook () => Actions.Player.Look.ReadValue<Vector2> ();

		public bool GetMousePress () => isMousePressed;

		public bool GetMouseClick () => isMouseClicked;

		public bool GetMouseSlowClick () => isMouseSlowClicked;

		public bool GetMouseHold () => isMouseHolding;

		public bool GetMouseReleased () => Actions.Player.Interact.ReadValue<float> () == 0;

		private void Pause_performed (InputAction.CallbackContext obj)
		{
			OnPauseClicked?.Invoke ();
		}

		public Vector3 GetMousePosition ()
		{
			return GetPlayerLook ();
		}

		private void Interact_canceled (UnityEngine.InputSystem.InputAction.CallbackContext obj)
		{
			Debug.Log ("Mouse released.");
			isMouseHolding = false;
			isMousePressed = false;
			isMouseSlowClicked = false;
			isMouseClicked = false;
		}

		private void Interact_performed (UnityEngine.InputSystem.InputAction.CallbackContext obj)
		{
			if (pressTime >= .5f)
			{
				Debug.Log ("Mouse slow clicked.");
				isMouseSlowClicked = true;
			}
			else
			{
				Debug.Log ("Mouse clicked.");
				isMouseClicked = true;
			}
		}

		private void Interact_started (UnityEngine.InputSystem.InputAction.CallbackContext obj)
		{
			Debug.Log ("Mouse pressed.");
			StartPressTimer ();
		}

		private void StartPressTimer ()
		{
			if (isMousePressed) return;

			pressTime = 0;
			isMousePressed = true;
		}
	}
}