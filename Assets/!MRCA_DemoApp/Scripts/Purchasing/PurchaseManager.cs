using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using BMJ.M360.Generic;
using UnityEngine.UI;

namespace BMJ.M360.Purchasing {
	public class PurchaseManager : InstancedMonoBehaviour<PurchaseManager> {
		[Header("Store Managers")]
		[SerializeField] List<StoreManager> StoreManagers;

		[Header("Purchase Panel")]
		[SerializeField] GameObject PurchasePanel;
		[SerializeField] Text ProductName, ProductDescription, ProductPrice;
		[SerializeField] Image ProductImage;

		StoreEntry CurrentStoreEntry = null;

		[Header("Cart")]
		[SerializeField] GameObject CartPanel;
		[SerializeField] Transform CartEntryHolder;
		[SerializeField] GameObject CartEntryPrefab;

		Dictionary<string, int> Cart = new Dictionary<string, int> ();
		Dictionary<string, StoreEntry> CartDetails = new Dictionary<string, StoreEntry> ();

		public static void ViewStoreEntry (string ItemID, int StoreIndex) {
			if (GetInstance == null) {
				print ("PurchaseManager instance not available.");
			} else {
				GetInstance.ProcessViewStoreEntry (ItemID, StoreIndex);
			}
		}

		void ProcessViewStoreEntry (string ItemID, int StoreIndex) {
			if (StoreIndex < StoreManagers.Count) {
				StoreManager TargetStore = StoreManagers [StoreIndex];

				if (TargetStore.CheckStoreLoaded) {
					StoreEntry TargetStoreEntry = null;

					TargetStoreEntry = TargetStore.GetStoreEntry (ItemID);

					if (TargetStoreEntry == null) {
						print ("StoreEntry [" + ItemID + "] not found.");
					} else {
						CurrentStoreEntry = TargetStoreEntry;

						ProductName.text = TargetStoreEntry.Name;
						ProductImage.sprite = TargetStoreEntry.PurchasePreview;
						ProductPrice.text = "RM " + TargetStoreEntry.Price;
						ProductDescription.text = TargetStoreEntry.Description;

						TogglePurchasePanel = true;
					}
				} else {
					print ("TargetStore not loaded.");
				}
			} else {
				print ("Invalid StoreIndex.");
			}
		}

		public void AddToCart () {
			if (CurrentStoreEntry == null) {
			} else {
				if (Cart.ContainsKey (CurrentStoreEntry.ID)) {
					Cart [CurrentStoreEntry.ID] = Cart [CurrentStoreEntry.ID] + 1;
				} else {
					Cart.Add (CurrentStoreEntry.ID, 1);
				}

				if (CartDetails.ContainsKey (CurrentStoreEntry.ID)) {
				} else {
					CartDetails.Add (CurrentStoreEntry.ID, CurrentStoreEntry);
				}

				CurrentStoreEntry = null;
				TogglePurchasePanel = false;
			}
		}

		bool TogglePurchasePanel {
			set {
				PurchasePanel.SetActive (value);
			}
		}

		public void DisplayCart () {
			foreach (Transform Child in CartEntryHolder) {
				Destroy (Child.gameObject);
			}

			foreach (KeyValuePair<string, int> CartEntryPair in Cart) {
				GameObject CartEntryObject = Instantiate (CartEntryPrefab, CartEntryHolder);
				CartEntryObject.transform.localScale = Vector3.one;

				CartEntryObject.GetComponent<CartEntry> ().Setup (CartDetails [CartEntryPair.Key].PurchasePreview, CartDetails [CartEntryPair.Key].Name, CartDetails [CartEntryPair.Key].Description, CartDetails [CartEntryPair.Key].Price.ToString (), CartEntryPair.Value.ToString ());
			}

			CartPanel.SetActive (true);
		}
	}
}