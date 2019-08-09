using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace BMJ.M360.MRCA.Timepiece {
	[System.Serializable]
	public class Timepiece {
		public string Name;
		public string Description;
		public string ExtraDetail;
		public float Price;
		public Sprite Thumbnail;

		[SerializeField] Text NameText;
		[SerializeField] Text DescriptionText;
		[SerializeField] Text ExtraDetailText;
		[SerializeField] Transform ColorSelectorHolder;

		public void Initialize () {
			if (NameText == null || DescriptionText == null || ExtraDetailText == null) {
				Debug.Log ("NameText : " + NameText + ", DescriptionText : " + DescriptionText + ", ExtraDetailText : " + ExtraDetailText);
			} else {
				NameText.text = Name;
				DescriptionText.text = Description;
				ExtraDetail = ExtraDetail.Replace ("/n", "\n");
				ExtraDetailText.text = ExtraDetail;

				if (ColorSelectorHolder.childCount > 0) {
					SelectColor = ColorSelectorHolder.GetChild (0);
				}
			}
		}

		public Transform SelectColor {
			set {
				if (value.parent.Equals (ColorSelectorHolder)) {
					foreach (Transform ColorSelector in ColorSelectorHolder) {
						ColorSelector.Find ("SelectionRing").gameObject.SetActive (false);
					}

					value.Find ("SelectionRing").gameObject.SetActive (true);
				}
			}
		}
	}
}
