using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SampleProjects.ObjectPicker
{
	public class InspectRaycast : MonoBehaviour
	{
		[SerializeField] float rayLength = 5f;
		[SerializeField] LayerMask layerMaskInteract;
		[SerializeField] InspectUI inspectUI;
		[SerializeField] InputHandler input;

		public delegate void HandleRaycastHit ();

		public static event HandleRaycastHit OnObjectEnter;
		public static event HandleRaycastHit OnObjectLeave;

		bool isHit;

		InteractableObject obj;

		// Start is called before the first frame update
		void Start ()
		{

		}

		// Update is called once per frame
		void Update ()
		{
			RaycastHit hit;
			Vector3 forward = transform.TransformDirection (Vector3.forward);

			if (Physics.Raycast (transform.position, forward, out hit, rayLength, layerMaskInteract.value))
			{
				if (hit.collider.CompareTag ("InteractObject"))
				{
					if (!isHit)
					{
						obj = hit.collider.gameObject.GetComponent<InteractableObject> ();

						if (!obj.IsInteractable) 
							return;

						inspectUI.Show (obj.Info);
						isHit = true;
						OnObjectEnter?.Invoke ();
					}

					if (input.GetMouseHold ())
					{
						obj.Hold ();
					}
					else if (input.GetMouseSlowClick ())
					{
						obj.DoLongAction ();
					}
					else if (input.GetMouseClick ())
					{
						obj.DoAction ();
					}
					else if (input.GetMouseReleased ())
					{
						obj.Release ();
					}
				}

				isHit = true;
			}
			else if (isHit)
			{
				isHit = false;
				inspectUI.Hide ();
				OnObjectLeave ();
			}
		}
	}
}