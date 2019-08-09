using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace BMJ.M360.MRCA.Footwear {
	[System.Serializable]
	public class Footwear {
		public string Name;
		public string Description;
		public string ExtraDetail;
		public float Price;
		public Sprite Thumbnail;

		[SerializeField] Text NameText;
		[SerializeField] Text DescriptionText;
		[SerializeField] Text ExtraDetailText;

		public void Initialize () {
			if (NameText == null || DescriptionText == null || ExtraDetailText == null) {
				Debug.Log ("NameText : " + NameText + ", DescriptionText : " + DescriptionText + ", ExtraDetailText : " + ExtraDetailText);
			} else {
				NameText.text = Name;
				DescriptionText.text = Description;
				ExtraDetail = ExtraDetail.Replace ("/n", "\n");
				ExtraDetailText.text = ExtraDetail;
			}
		}
		
	}
}
