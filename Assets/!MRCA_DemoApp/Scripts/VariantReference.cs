using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace BMJ.M360.MRCA {
	[System.Serializable]
	public class VariantReference {
		public string Name;
		public GameObject Variant;
		public GameObject SelectionIndicator;

		public bool Toggle {
			set {
				Variant.SetActive (value);
				SelectionIndicator.SetActive (value);
			}
		}
	}
}
