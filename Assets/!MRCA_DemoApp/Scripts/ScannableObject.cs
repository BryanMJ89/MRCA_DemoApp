using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using BMJ.M360.Purchasing;

namespace BMJ.M360.MRCA {
	public class ScannableObject : MonoBehaviour {
		[SerializeField] string ItemID;
		[SerializeField] int StoreIndex;

		public void ViewStoreEntry () {
			PurchaseManager.ViewStoreEntry (ItemID, StoreIndex);
		}
	}
}
