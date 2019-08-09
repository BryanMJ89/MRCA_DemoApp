using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace BMJ.M360.MRCA.Cart {
	public class SummaryEntry : MonoBehaviour {
		[SerializeField] Text Name;
		[SerializeField] Text Quantity;
		[SerializeField] Text Cost;

		public void Initialize (string SetName, int SetQuantity, float SetCost) {
			Name.text = SetName;
			Quantity.text = "x" + SetQuantity;
			Cost.text = "RM " + SetCost.ToString ("F");
		}
	}
}