using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using BMJ.M360.MRCA.Cart;

namespace BMJ.M360.MRCA.Timepiece {
	public class TimepieceCustomizationController : CustomizationController {
		[Header("Timepiece Variables")]
		[SerializeField] Timepiece Data;

		protected override void Initialize () {
			Data.Initialize ();

			base.Initialize ();
		}

		protected override void UpdateSingleOrderPrice () {
			float UpdatedPricing = Data.Price;

			SingleOrderPrice = UpdatedPricing;

			base.UpdateSingleOrderPrice ();
		}

		public Transform SelectColor {
			set {
				Data.SelectColor = value;
			}
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
