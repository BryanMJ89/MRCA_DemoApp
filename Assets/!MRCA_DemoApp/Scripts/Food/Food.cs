using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace BMJ.M360.MRCA.Food {
	[System.Serializable]
	public class Food {
		public string Name;
		public string Description;
		public float Price;
		public List<Condiment> Condiments;
		public Sprite Thumbnail;

		[SerializeField] Text NameText;
		[SerializeField] Text DescriptionText;

		public void Initialize () {
			if (NameText == null || DescriptionText == null) {
				Debug.Log ("NameText : " + NameText + ", DescriptionText : " + DescriptionText);
			} else {
				NameText.text = Name;
				DescriptionText.text = Description;
			}

			foreach (Condiment TargetCondiment in Condiments) {
				TargetCondiment.Initialize ();
			}
		}
	}
}
