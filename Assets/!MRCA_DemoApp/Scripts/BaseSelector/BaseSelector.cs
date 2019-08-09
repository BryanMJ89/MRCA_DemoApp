
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

using BMJ.M360.Generic;
using BMJ.M360.Purchasing;

namespace BMJ.M360.MRCA.BaseSelectors {
	public class BaseSelector : MonoBehaviour {
		[SerializeField] Animator ScannableSwipeAnimator;
		[SerializeField] List<GameObject> ProductLineup;

		[SerializeField] Material HDRIMaterial;
		[SerializeField] float AmbientIntensity;
		[SerializeField] float ReflectionIntensity;

		[SerializeField] InputProcessor AttachedSwipeProcessor;

		int CurrentIndex = 1;

		bool ManualSetCurrentIndex = false;

		const string Left = "Left";
		const string Right = "Right";
		const string Up = "Up";
		const string Down = "Down";

		void Start () {
			AttachedSwipeProcessor.RegisterInputActions (
				(DetectedMoveDirection) => {
					ProcessInput (DetectedMoveDirection);
				}
			);

			HDRI_Updater.UpdateMap (HDRIMaterial, AmbientIntensity, ReflectionIntensity);
		}

		void ProcessInput (MoveDirection DetectedMoveDirection) {
			switch (DetectedMoveDirection) {
			case MoveDirection.Left:
				if (CurrentIndex > 0) {
					CurrentIndex--;
				}
				break;
			case MoveDirection.Right:
				if (CurrentIndex < ProductLineup.Count - 1) {
					CurrentIndex++;
				}
				break;
			case MoveDirection.Up:
				break;
			case MoveDirection.Down:
				break;
			case MoveDirection.None:
				if (BaseSelectorDetector.GetMouseOver) {
					TriggerViewProduct ();
				} else {
					print ("Click not on detector!");
				}
				break;
			}

			if (DetectedMoveDirection.Equals (MoveDirection.None)) {
			} else {
				if (ScannableSwipeAnimator == null) {
				} else {
					ScannableSwipeAnimator.SetInteger ("Index", CurrentIndex);
				}

				UpdateProductModel ();
			}
		}

		void UpdateProductModel () {
			CustomizationController CustomizationControllerReference = ProductLineup [CurrentIndex].GetComponent<CustomizationController> ();
			HDRI_Updater.UpdateMap (CustomizationControllerReference.GetHDRIMaterial, CustomizationControllerReference.GetAmbientIntensity, CustomizationControllerReference.GetReflectionIntensity);
		}

		public void TriggerViewProduct () {
			if (ManualSetCurrentIndex) {
			} else {
				CurrentIndex = 0;
			}

			if (ProductLineup [CurrentIndex] == null) {
				print ("Product Lineup [" + CurrentIndex + "] not found!"); 
			} else {
				print ("Instantiating : " + ProductLineup [CurrentIndex]);
				Instantiate (ProductLineup [CurrentIndex]);
			}
		}

		public int SetCurrentIndex {
			set {
				ManualSetCurrentIndex = true;
				CurrentIndex = value;
			}
		}
	}
}
