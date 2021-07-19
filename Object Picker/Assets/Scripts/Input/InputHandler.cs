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

			GameController.OnGameStart += UnlockControls;
			GameController.OnGameOver += LockControls;
		}

		private void OnDisable ()
		{
			inputActions.Disable ();
			inputActions.Player.Interact.started -= Interact_started;

			GameController.OnGameStart -= UnlockControls;
			GameController.OnGameOver -= LockControls;
		}

		private void Update ()
		{
			if (isMousePressed)
			{
				pressTime += Time.deltaTime;
			}
		}

		public Vector2 GetPlayerMovement () => GetMovementInput ();

		public Vector2 GetPlayerLook () => GetMousePosition ();

		public bool GetMousePress () => isMousePressed && controlEnabled;

		public bool GetMouseClick () => isMouseClicked && controlEnabled;

		public bool GetMouseSlowClick () => isMouseSlowClicked && controlEnabled;

		public bool GetMouseHold () => isMouseHolding && controlEnabled;

		public bool GetMouseReleased () => Actions.Player.Interact.ReadValue<float> () == 0 && controlEnabled;

		private void Pause_performed (InputAction.CallbackContext obj)
		{
			if (controlEnabled)
				OnPauseClicked?.Invoke ();
		}

		private Vector3 GetMovementInput ()
		{
			if (controlEnabled)
				return Actions.Player.Movement.ReadValue<Vector2> ();
			else return Vector3.zero;
		}

		private Vector3 GetMousePosition ()
		{
			if (controlEnabled)
				return Actions.Player.Look.ReadValue<Vector2> ();
			else return Vector3.zero;
		}

		private void Interact_canceled (InputAction.CallbackContext obj)
		{
			Debug.Log ("Mouse released.");
			isMouseHolding = false;
			isMousePressed = false;
			isMouseSlowClicked = false;
			isMouseClicked = false;
		}

		private void Interact_performed (InputAction.CallbackContext obj)
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

		private void Interact_started (InputAction.CallbackContext obj)
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

		private void LockControls ()
		{
			controlEnabled = false;
		}

		private void UnlockControls ()
		{
			controlEnabled = true;
		}
	}
}