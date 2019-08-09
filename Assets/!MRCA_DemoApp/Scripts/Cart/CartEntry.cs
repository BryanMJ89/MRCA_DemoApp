using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using BMJ.M360.Generic;

namespace BMJ.M360.MRCA.Cart {
	public class CartEntry : MonoBehaviour {
		[SerializeField] Transform CustomPropertyHolder;
		[SerializeField] GameObject CustomPropertyPrefab;

		[SerializeField] Image Thumbnail;

		[SerializeField] Text Name;
		[SerializeField] Text Description;
		[SerializeField] Text Total;

		public GameObject Customizer;

		public void Initialize (List<string> CustomProperties, GameObject SetCustomizer, Sprite SetThumbnail, string SetName, string SetDescription) {
			Customizer = SetCustomizer;

			foreach (string CustomProperty in CustomProperties) {
				GameObject CustomPropertyObject = Instantiate (CustomPropertyPrefab, CustomPropertyHolder);
				CustomPropertyObject.GetComponentInChildren<Text> ().text = CustomProperty;
				if (SetName.Length > 20) {
					SetName = SetName.Substring (0, 20);
					SetName += "...";
				}

				Name.text = SetName;
				Description.text = SetDescription;
				Total.text = "RM " + Customizer.GetComponent<CustomizationController> ().GetOrderCost;
			}

			Thumbnail.sprite = SetThumbnail;
		}

		public string GetName {
			get { 
				return Name.text;
			}
		}

		public int GetQuantity {
			get { 
				return Customizer.GetComponent<CustomizationController> ().GetOrderQuantity;
			}
		}

		public void Edit () {
			CartManager.Edit (
				() => {
					Customizer.SetActive (true);
					Customizer.GetComponent<CustomizationController> ().UpdateHDRI ();
					Destroy (gameObject);
				},
				() => {
					Delete ();
				},
				Name.text
			);
		}

		public float GetCost {
			get { 
				return Customizer.GetComponent<CustomizationController> ().GetOrderCost;
			}
		}

		public void Delete () {
			Destroy (Customizer);
			Destroy (gameObject);
		}
	}
}
