using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace BMJ.M360.Purchasing {
	[System.Serializable]
	public class StoreResponse {
		public int Result;
		public List<Purchasable> Store;
	}

	[System.Serializable]
	public class Purchasable {
		public string ID;
		public string Name;
		public string PuchasePreviewLink;
		public float Price;
		public string Description;
	}
}