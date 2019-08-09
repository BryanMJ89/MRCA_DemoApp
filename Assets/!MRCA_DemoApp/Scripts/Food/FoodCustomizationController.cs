using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using BMJ.M360.MRCA.Cart;

namespace BMJ.M360.MRCA.Food {
	public class FoodCustomizationController : CustomizationController {
		[Header("Food Variables")]
		[SerializeField] Food Data;

		List<Condiment> OrderedCondiments = new List<Condiment> ();

		protected override void Initialize () {
			Data.Initialize ();

			base.Initialize ();
		}

		public void ToggleCondiment (Toggle TargetToggle) {
			Condiment TargetCondiment = Data.Condiments [TargetToggle.transform.parent.GetSiblingIndex ()];;

			if (TargetCondiment == null) {
				print ("[" + TargetToggle.name + "] condiment does not exist!");
				TargetToggle.isOn = false;
			} else {
				if (TargetToggle.isOn && !OrderedCondiments.Contains (TargetCondiment)) {
					OrderedCondiments.Add (TargetCondiment);
					if (TargetCondiment.AttachedAnimator == null) {
					} else {
						TargetCondiment.AttachedAnimator.SetBool ("On", true);
					}
				} else if (!TargetToggle.isOn && OrderedCondiments.Contains (TargetCondiment)) {
					OrderedCondiments.Remove (TargetCondiment);
					if (TargetCondiment.AttachedAnimator == null) {
					} else {
						TargetCondiment.AttachedAnimator.SetBool ("On", false);
					}
				}
				TargetToggle.transform.Find ("ON").gameObject.SetActive (TargetToggle.isOn);
				TargetToggle.transform.Find ("OFF").gameObject.SetActive (!TargetToggle.isOn);

				UpdateSingleOrderPrice ();
			}
		}

		protected override void UpdateSingleOrderPrice () {
			float UpdatedPricing = Data.Price;

			foreach (Condiment OrderedCondiment in OrderedCondiments) {
				UpdatedPricing += OrderedCondiment.Price;
			}

			SingleOrderPrice = UpdatedPricing;

			base.UpdateSingleOrderPrice ();
		}

		public override void AddToCart () {
			if (OrderQuantity > 0) {
				List<string> CustomProperties = new List<string> ();

				CustomProperties.Add ("Item x " + OrderQuantity);

				foreach (Condiment OrderedCondiment in OrderedCondiments) {
					CustomProperties.Add (OrderedCondiment.Name);
				}

				CartManager.AddToCart (CustomProperties, gameObject, Data.Thumbnail, Data.Name, Data.Description);

				base.AddToCart ();
			}
		}
	}
}
