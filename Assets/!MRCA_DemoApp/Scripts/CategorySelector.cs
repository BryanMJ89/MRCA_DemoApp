using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using BMJ.M360.Generic;
using BMJ.M360.MRCA.ManualSelection;

namespace BMJ.M360.MRCA.Food {
	public class CategorySelector : InstancedMonoBehaviour<CategorySelector> {
		[SerializeField] Transform CategoryButtonHolder;
		[SerializeField] GameObject ManualViewButton;
		[SerializeField] TableSelection AttachedTableSelectionController;

		[System.Serializable]
		public class ManualSelectorDefiner {
			public Category DefinedCategory;
			public Animator DefinedAnimator;
			public GameObject DefinedCatalogue;
			public ScrollRect DefinedScrollRect;

			public bool ToggleSelector {
				set {
					DefinedAnimator.SetBool ("Toggle", value);
				}
			}

			public bool ToggleCatalogue {
				set { 
					DefinedCatalogue.SetActive (value);
				}
			}
		}

		[SerializeField] List<ManualSelectorDefiner> ManualSelectors;

		Category SelectedCategory = Category.NONE;

		List<Button> CategoryButtons = new List<Button> ();

		bool ManualViewStatus = false;

		public static bool ToggleManualViewButton {
			set { 
				if (GetInstance == null) {
				} else {
					if (GetInstance.SelectedCategory.Equals (Category.NONE)) {
						GetInstance.ManualViewButton.SetActive (false);
					} else {
						GetInstance.ManualViewButton.SetActive (value);
					}
				}
			}
		}

		public static bool GetManualViewStatus {
			get {
				if (GetInstance == null) {
					return false;
				} else {
					return GetInstance.ManualViewStatus;
				}
			}
		}

		void Start () {
			foreach (Button ParsedButton in CategoryButtonHolder.GetComponentsInChildren<Button>()) {
				CategoryButtons.Add (ParsedButton);
			}

			ToggleManualViewButton = false;
		}

		public void ClearSelectedCategory () {
			MRCA_TrackableEventHandler.ClearTrackables ();

			SelectedCategory = Category.NONE;

			ToggleManualViewButton = false;

			foreach (Button CategoryButton in CategoryButtons) {
				CategoryButton.interactable = true;
			}

			ManualSelector.ClearObjects ();
		}

		public Button SelectCategory {
			set {
				MRCA_TrackableEventHandler.ClearTrackables ();

				if (CategoryButtons.Contains (value)) {
					SelectedCategory = (Category)CategoryButtons.IndexOf (value);

					foreach (Button CategoryButton in CategoryButtons) {
						CategoryButton.interactable = !CategoryButton.Equals (value);
					}
				}

				ManualSelector.ClearObjects ();

				foreach (ManualSelectorDefiner ParsedManualSelector in ManualSelectors) {
					ParsedManualSelector.ToggleCatalogue = ParsedManualSelector.DefinedCategory.Equals (SelectedCategory);
				}

				AttachedTableSelectionController.ToggleTableSelectionPanel = SelectedCategory.Equals (Category.DINING);
			}
		}

		public static bool CheckCategory (Category TargetCategory) {
			if (GetInstance == null) {
				return false;
			} else {
				return GetInstance.SelectedCategory.Equals (TargetCategory);
			}
		}

		public static bool StaticManualViewCatalogue {
			set { 
				if (GetInstance == null) {
				} else {
					GetInstance.ManualViewCatalogue = value;
				}
			}
		}

		public bool ManualViewCatalogue {
			set {
				print ("TRACE MANUAL VIEW STATUS : " + ManualViewStatus);
				ManualSelectorDefiner TargetManualSelector = ManualSelectors.Find (manualSelector => manualSelector.DefinedCategory.Equals (SelectedCategory));

				ManualViewStatus = value;

				CanvasController.ToggleScanObject = !value;

				ToggleManualViewButton = true;

				ManualViewButton.transform.Find ("ON").gameObject.SetActive (value);
				ManualViewButton.transform.Find ("OFF").gameObject.SetActive (!value);

				if (value) {
				} else {
					ManualSelector.ClearObjects ();
				}

				if (TargetManualSelector == null) {
				} else {
					TargetManualSelector.ToggleSelector = value;
				}
			}
		}
	}
}
