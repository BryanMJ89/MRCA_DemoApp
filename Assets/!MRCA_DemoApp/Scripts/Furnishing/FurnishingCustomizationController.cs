using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using BMJ.M360.MRCA.Cart;

namespace BMJ.M360.MRCA.Furnishing {
	public class FurnishingCustomizationController : CustomizationController {
		[Header("Furnishing Variables")]
		[SerializeField] Furnishing Data;

		protected override void Initialize () {
			Data.Initialize ();

			base.Initialize ();
		}

		protected override void UpdateSingleOrderPrice () {
			float UpdatedPricing = Data.Price;

			SingleOrderPrice = UpdatedPricing;

			base.UpdateSingleOrderPrice ();
		}

		public Transform SelectVariant {
			set {
				Data.SelectVariant = value;
			}
		}

		public override void AddToCart () {
			if (OrderQuantity > 0) {
				List<string> CustomProperties = new List<string> ();

				CustomProperties.Add ("Item x " + OrderQuantity);

				CustomProperties.Add (Data.GetVariantName);

				CartManager.AddToCart (CustomProperties, gameObject, Data.Thumbnail, Data.Name, Data.Description);

				base.AddToCart ();
			}
		}
	}
}
