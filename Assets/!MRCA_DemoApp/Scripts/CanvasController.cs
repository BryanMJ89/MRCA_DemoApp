using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using BMJ.M360.Generic;
using BMJ.M360.MRCA.Food;

namespace BMJ.M360.MRCA {
	public class CanvasController : InstancedMonoBehaviour<CanvasController> {
		[Header("Generic Accessors")]
		[SerializeField] Transform ARCamera;

		[Header("Scan Canvas")]
		[SerializeField] GameObject ScanCanvasObject;
		int TrackedObjects = 0;

		void Start () {
			Vuforia.CameraDevice.Instance.SetFocusMode(Vuforia.CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		}

		public static void InitializeARCameraPosition () {
			if (GetInstance == null) {
			} else {
				if (GetInstance.ARCamera == null) {
				} else {
					GetInstance.ARCamera.position = Vector3.zero;
					GetInstance.ARCamera.rotation = Quaternion.identity;
					GetInstance.ARCamera.localScale = Vector3.one;
				}
			}
		}

		public static Transform GetInstantiatePoint {
			get { 
				if (GetInstance == null) {
					return new GameObject ().transform;
				} else {
					return GetInstance.ARCamera;
				}
			}
		}

		public static bool ToggleScanObject {
			set {
				if (GetInstance == null) {
				} else {
					GetInstance.ProcessToggleScanObject = value;
				}
			}
		}

		bool ProcessToggleScanObject {
			set {
				TrackedObjects += value.Equals (true) ? -1 : 1;

				if (TrackedObjects > 0) {
				} else {
					TrackedObjects = 0;
				}

				ScanCanvasObject.SetActive (TrackedObjects.Equals (0));

				CategorySelector.ToggleManualViewButton = !TrackedObjects.Equals (0);

				print ("TrackedObjects : [" + value + "] " + TrackedObjects);
			}
		}
	}
}
