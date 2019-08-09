using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using BMJ.M360.MRCA.BaseSelectors;

namespace BMJ.M360.MRCA.ManualSelection {
	[RequireComponent(typeof(Button))]
	public class ManualSelector : MonoBehaviour {
		[SerializeField] GameObject Product;

		Button AttachedButton;

		GameObject ActiveProduct = null;

		void Start () {
			AttachedButton = GetComponent<Button> ();
		}

		public void ManualView () {
			print ("Toggling to : " + transform.name + " : " + transform.GetSiblingIndex ());
			FindObjectOfType<SelectorAnimatorAccessor> ().ManualSelect = transform.GetSiblingIndex ();

			foreach (Button Selector in transform.parent.GetComponentsInChildren<Button> ()) {
				Selector.interactable = !(Selector.gameObject.Equals (gameObject));
			}

			print ("Setting current index to : " + transform.GetSiblingIndex ());

			FindObjectOfType<BaseSelector> ().SetCurrentIndex = transform.GetSiblingIndex ();
		}

		public bool ViewObject {
			set {
				if (value && ActiveProduct == null) {
					ClearObjects ();
					CanvasController.InitializeARCameraPosition ();
					ActiveProduct = Instantiate (Product, CanvasController.GetInstantiatePoint);
				} else if (!value && !(ActiveProduct == null)) {
					Destroy (ActiveProduct);
					ActiveProduct = null;
				}

				AttachedButton.interactable = !value;
			}
		}

		public static void ClearObjects () {
			//BaseSelectorDetector.DestroyInstance ();

			foreach (ManualSelector Selector in FindObjectsOfType<ManualSelector>()) {
				Selector.ViewObject = false;
			}
		}
	}
}
