using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace BMJ.M360.Purchasing {
	[System.Serializable]
	public class StoreEntry {
		public string ID;
		public string Name;
		public Sprite PurchasePreview;
		public float Price;
		public string Description;

		private StoreEntry () {
		}

		public StoreEntry (string SetID, string SetName, Sprite SetPuchasePreview, float SetPrice, string SetDescription) {
			ID = SetID;
			Name = SetName;
			PurchasePreview = SetPuchasePreview;
			Price = SetPrice;
			Description = SetDescription;
		}
	}
}
