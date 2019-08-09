using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace BMJ.M360.Purchasing {
	public class CartEntry : MonoBehaviour {
		[SerializeField] Image Thumbnail;
		[SerializeField] Text Name;
		[SerializeField] Text Description;
		[SerializeField] Text Price;
		[SerializeField] Text Quantity;

		public void Setup (Sprite SetThumbnail, string SetName, string SetDescription, string SetPrice, string SetQuantity) {
			Thumbnail.sprite = SetThumbnail;
			Name.text = SetName;
			Description.text = SetDescription;
			Price.text = SetPrice;
			Quantity.text = SetQuantity;
		}
	}
}
