using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using BMJ.M360.MRCA;
using BMJ.M360.MRCA.Cart;

namespace BMJ.M360.MRCA.Footwear {
	public class FootwearCustomizationController : CustomizationController {

		[Header("Footwear Variables")]
		[SerializeField] Footwear Data;

		protected override void Initialize () {
			Data.Initialize ();

			base.Initialize ();
		}

		protected override void UpdateSingleOrderPrice () {
			float UpdatedPricing = Data.Price;

			SingleOrderPrice = UpdatedPricing;

			base.UpdateSingleOrderPrice ();
		}

		public override void AddToCart () {
			if (OrderQuantity > 0) {
				List<string> CustomProperties = new List<string> ();

				CustomProperties.Add ("Item x " + OrderQuantity);

				CartManager.AddToCart (CustomProperties, gameObject, Data.Thumbnail, Data.Name, Data.Description);

				base.AddToCart ();
			}
		}
	}
}
