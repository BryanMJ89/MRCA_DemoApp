using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace BMJ.M360.MRCA.Furnishing {
	[System.Serializable]
	public class Furnishing {
		public string Name;
		public string Description;
		public string ExtraDetail;
		public float Price;
		public Sprite Thumbnail;

		[SerializeField] Text NameText;
		[SerializeField] Text DescriptionText;
		[SerializeField] Text ExtraDetailText;
		[SerializeField] List<VariantReference> VariantReferences;

		int SelectedVariantIndex = 0;

		public void Initialize () {
			if (NameText == null || DescriptionText == null || ExtraDetailText == null) {
				Debug.Log ("NameText : " + NameText + ", DescriptionText : " + DescriptionText + ", ExtraDetailText : " + ExtraDetailText);
			} else {
				NameText.text = Name;
				DescriptionText.text = Description;
				ExtraDetail = ExtraDetail.Replace ("/n", "\n");
				ExtraDetailText.text = ExtraDetail;

				foreach (VariantReference ParsedVariantReference in VariantReferences) {
					ParsedVariantReference.Toggle = false;
				}

				if (VariantReferences.Count > 0) {
					VariantReferences [0].Toggle = true;
				}
			}
		}

		public Transform SelectVariant {
			set {
				if (value.GetSiblingIndex () < VariantReferences.Count) {
					foreach (VariantReference Variant in VariantReferences) {
						Variant.Toggle = false;
					}

					VariantReferences [value.GetSiblingIndex ()].Toggle = true;
					SelectedVariantIndex = value.GetSiblingIndex ();
				}
			}
		}

		public string GetVariantName {
			get {
				return VariantReferences [SelectedVariantIndex].Name;
			}
		}
	}
}
