using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using BMJ.M360.Purchasing;

namespace BMJ.M360.Generic {
	public class InputProcessor : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
		[Header("Swipe Processor")]
		[SerializeField] float Sensitivity;

		delegate void InputAction (MoveDirection DetectedMoveDirection);
		InputAction OnInputAction;

		public void RegisterInputActions (UnityAction<MoveDirection> SetInputAction) {
			OnInputAction += SetInputAction.Invoke;
		}

		public void DeregisterInputActions (UnityAction<MoveDirection> SetInputAction) {
			OnInputAction -= SetInputAction.Invoke;
		}

		public enum SwipeAxis {
			X = 0,
			Y = 1,
			XY = 2,
			None = 3
		}

		[SerializeField] protected SwipeAxis DefinedSwipeAxis;

		Vector3 PointerDownPosition = Vector3.zero;

		void Start () {
			Initialize ();
		}

		protected virtual void Initialize () {
			
		}

		public void OnPointerDown (PointerEventData eventData) {
			PointerDownPosition = Input.mousePosition;
		}

		public void OnPointerUp (PointerEventData eventData) {
			Vector3 SwipeVector = Input.mousePosition - PointerDownPosition;

			if (SwipeVector.magnitude < (1.0f / Sensitivity)) {
				ProcessClick ();
			} else {
				switch (DefinedSwipeAxis) {
				case SwipeAxis.X:
					ProcessHorizontalSwipe (SwipeVector);
					break;
				case SwipeAxis.Y:
					ProcessVerticalSwipe (SwipeVector);
					break;
				case SwipeAxis.XY:
					if (Mathf.Abs (SwipeVector.x) > Mathf.Abs (SwipeVector.y)) {
						ProcessHorizontalSwipe (SwipeVector);
					} else {
						ProcessVerticalSwipe (SwipeVector);
					}
					break;
				case SwipeAxis.None:
					ProcessClick ();
					break;
				}
			}
		}

		void ProcessClick () {
			OnInputAction.Invoke (MoveDirection.None);
		}

		void ProcessHorizontalSwipe (Vector3 SwipeVector) {
			if (SwipeVector.x > 0.0f) {
				ProcessSwipe (MoveDirection.Left);
			} else {
				ProcessSwipe (MoveDirection.Right);
			}
		}

		void ProcessVerticalSwipe (Vector3 SwipeVector) {
			if (SwipeVector.y > 0.0f) {
				ProcessSwipe (MoveDirection.Up);
			} else {
				ProcessSwipe (MoveDirection.Down);
			}
		}

		void ProcessSwipe (MoveDirection DetectedMoveDirection) {
			print ("Swipe Detected : " + DetectedMoveDirection.ToString ());

			OnInputAction.Invoke (DetectedMoveDirection);
		}
	}
}

