using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace BMJ.M360.MRCA.Food {
	[System.Serializable]
	public class Condiment {
		public string Name;
		public string Description;
		public float Price;
		public Animator AttachedAnimator;
		public Sprite On;
		public Sprite Off;

		[SerializeField] Transform InfoPanel;

		Text NameText;
		Text DescriptionText;
		Text PriceText;
		Image ToggleImageOn;
		Image ToggleImageOff;

		public void Initialize () {
			NameText = InfoPanel.Find ("Name").GetComponent<Text> ();
			DescriptionText = InfoPanel.Find ("Description").GetComponent<Text> ();
			PriceText = InfoPanel.Find ("Price").GetComponent<Text> ();

			ToggleImageOn = InfoPanel.Find ("Toggle").Find ("ON").GetComponent<Image> ();
			ToggleImageOff = InfoPanel.Find ("Toggle").Find ("OFF").GetComponent<Image> ();

			if (NameText == null || DescriptionText == null || PriceText == null || ToggleImageOn == null || ToggleImageOff == null) {
				Debug.Log ("NameText : " + NameText + ", DescriptionText : " + DescriptionText + ", PriceText : " + PriceText + ", ToggleImageOn : " + ToggleImageOn + ", ToggleImageOff : " + ToggleImageOff);
			} else {
				NameText.text = Name;
				DescriptionText.text = Description;
				PriceText.text = "RM " + Price.ToString ("F");

				ToggleImageOn.sprite = On;
				ToggleImageOff.sprite = Off;
			}
		}
	}
}
