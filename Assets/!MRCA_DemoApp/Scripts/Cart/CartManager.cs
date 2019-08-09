using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using BMJ.M360.Generic;

namespace BMJ.M360.MRCA.Cart {
	public class CartManager : InstancedMonoBehaviour<CartManager> {
		[SerializeField] Transform CartEntryHolder;
		[SerializeField] GameObject CartEntryPrefab;

		[SerializeField] Text FloatingItemCountUIText;
		[SerializeField] Text ItemCountUIText;
		[SerializeField] Text GrandTotalUIText;
		[SerializeField] Text SummaryGrandTotalUIText;

		[SerializeField] GameObject CartPanel;

		float GrandTotal = 0.0f;

		UnityAction CurrentEditAction = null;
		UnityAction CurrentDeleteAction = null;

		[SerializeField] GameObject EditPrompt;
		[SerializeField] Text EditPromptText;

		[Header("Summary")]
		[SerializeField] GameObject SummaryEntryPrefab;
		[SerializeField] GameObject SummaryPanel;
		[SerializeField] Transform SummaryEntryHolder;

		[Header("CompletePurchase")]

		[SerializeField] GameObject CompletePurchasePanel;

		public bool ToggleEditPrompt {
			set {
				if (value) {
				} else {
					CurrentEditAction = null;
					CurrentDeleteAction = null;
				}
				EditPrompt.SetActive (value);
			}
		}

		public static void Edit (UnityAction EditAction, UnityAction DeleteAction, string EditableName) {
			if (GetInstance == null) {
			} else {
				GetInstance.ProcessEdit (EditAction, DeleteAction, EditableName);
			}
		}

		void ProcessEdit (UnityAction EditAction, UnityAction DeleteAction, string EditableName) {
			EditPromptText.text = "Edit \"" + EditableName + "\"";
			CurrentEditAction = EditAction;
			CurrentDeleteAction = DeleteAction;
			ToggleEditPrompt = true;
		}

		public void DoEdit () {
			if (CurrentEditAction == null) {
			} else {
				CurrentEditAction.Invoke ();
				CurrentEditAction = null;
				CurrentDeleteAction = null;
			}

			ToggleEditPrompt = false;
		}

		public void DoDelete () {
			if (CurrentDeleteAction == null) {
			} else {
				CurrentDeleteAction.Invoke ();
				CurrentEditAction = null;
				CurrentDeleteAction = null;
			}

			ToggleEditPrompt = false;
		}

		public static void AddToCart (List<string> CustomProperties, GameObject Customizer, Sprite SetThumbnail, string SetName, string SetDescription) {
			if (GetInstance == null) {
			} else {
				GetInstance.ProcessAddToCart (CustomProperties, Customizer, SetThumbnail, SetName, SetDescription);
			}
		}

		void ProcessAddToCart (List<string> CustomProperties, GameObject Customizer, Sprite SetThumbnail, string SetName, string SetDescription) {
			GameObject CartEntryObject = Instantiate (CartEntryPrefab, CartEntryHolder);
			CartEntryObject.GetComponent<CartEntry> ().Initialize (CustomProperties, Customizer, SetThumbnail, SetName, SetDescription);
		}

		void Update () {
			UpdatePurchaseItemCount ();
			UpdateGrandTotal ();
		}

		void UpdatePurchaseItemCount () {
			FloatingItemCountUIText.text = CartEntryHolder.childCount.ToString ();
			ItemCountUIText.text = CartEntryHolder.childCount + " items for purchase";
		}

		void UpdateGrandTotal () {
			GrandTotal = 0.0f;

			foreach (CartEntry ParsedCartEntry in CartEntryHolder.GetComponentsInChildren<CartEntry> ()) {
				GrandTotal += ParsedCartEntry.GetCost;
			}

			GrandTotalUIText.text = "RM " + (GrandTotal).ToString ("F");
			SummaryGrandTotalUIText.text = "RM " + (GrandTotal).ToString ("F");
		}

		public bool ToggleCart {
			set {
				CartPanel.SetActive (value);
			}
		}

		public void GoToSummary () {
			foreach (CartEntry ParsedCartEntry in CartEntryHolder.GetComponentsInChildren<CartEntry> ()) {
				GameObject SummaryEntryInstance = Instantiate (SummaryEntryPrefab, SummaryEntryHolder);

				SummaryEntryInstance.GetComponent<SummaryEntry> ().Initialize (ParsedCartEntry.GetName, ParsedCartEntry.GetQuantity, ParsedCartEntry.GetCost);
			}

			ToggleSummaryPanel = true;
		}

		public bool ToggleSummaryPanel {
			set {
				SummaryPanel.SetActive (value);
			}
		}

		public void Done () {
			foreach (CartEntry ParsedCartEntry in CartEntryHolder.GetComponentsInChildren<CartEntry> ()) {
				ParsedCartEntry.Delete ();
			}

			CompletePurchasePanel.SetActive (true);
		}

		public void RestartFlow () {
			ToggleCart = false;
			ToggleSummaryPanel = false;

			CompletePurchasePanel.SetActive (false);
		}
	}
}