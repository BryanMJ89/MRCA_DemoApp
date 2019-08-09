using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using BMJ.M360.MRCA.Food;
using BMJ.M360.MRCA.ManualSelection;

namespace BMJ.M360.MRCA {
	public class MRCA_TrackableEventHandler : DefaultTrackableEventHandler {
		[System.Serializable]
		public class MRCA_TrackableCategoryVariables {
			public int PrefabIndex;
			public Category SetCategory = Category.DINING;
		}

		[SerializeField] List<MRCA_TrackableCategoryVariables> CategoryVariables;

		GameObject CurrentScannableObject = null;

		public static void ClearTrackables () {
			foreach (MRCA_TrackableEventHandler Tracker in FindObjectsOfType<MRCA_TrackableEventHandler>()) {
				Tracker.OnTrackingLost ();
			}
		}

		protected override void OnTrackingFound () {
			if (ScannableObjectsManager.GetScannableObjects == null) {
			} else {
				foreach (MRCA_TrackableCategoryVariables CategoryVariable in CategoryVariables) {
					print (gameObject.name + " checking against : " + CategoryVariable.SetCategory);
					if (CategorySelector.CheckCategory (CategoryVariable.SetCategory)) {
						if (CategoryVariable.PrefabIndex < ScannableObjectsManager.GetScannableObjects.Count) {
							CurrentScannableObject = Instantiate (ScannableObjectsManager.GetScannableObjects [CategoryVariable.PrefabIndex]);
							CurrentScannableObject.transform.localPosition = Vector3.zero;
							CurrentScannableObject.transform.localRotation = Quaternion.identity;
							CurrentScannableObject.transform.localScale = Vector3.one;

							foreach (ManualSelector ParsedSelection in FindObjectsOfType<ManualSelector> ()) {
								if (ParsedSelection.transform.GetSiblingIndex ().Equals (1) && ParsedSelection.gameObject.activeSelf) {
									ParsedSelection.GetComponent<Button> ().onClick.Invoke ();
								}
							}

							CategorySelector.StaticManualViewCatalogue = true;
						}

						CanvasController.ToggleScanObject = false;

						base.OnTrackingFound ();

						break;
					}
				}
			}
		}

		protected override void OnTrackingLost () {
			if (CurrentScannableObject == null) {
			} else {
				Destroy (CurrentScannableObject);

				CategorySelector.StaticManualViewCatalogue = false;
				CanvasController.ToggleScanObject = true;
			}

			base.OnTrackingLost ();
		}
	}
}
