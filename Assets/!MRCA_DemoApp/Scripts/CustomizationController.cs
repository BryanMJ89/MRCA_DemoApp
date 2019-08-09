using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using BMJ.M360.MRCA.Cart;

namespace BMJ.M360.MRCA {
	public class CustomizationController : MonoBehaviour {
		[Header("Customization Panel")]
		[SerializeField] Text Quantity;
		[SerializeField] Text PriceText;

		[SerializeField] Material HDRIMaterial;
		[SerializeField] float AmbientIntensity;
		[SerializeField] float ReflectionIntensity;

		bool CartEditMode = false;

		public Material GetHDRIMaterial {
			get {
				return HDRIMaterial;
			}
		}

		public float GetAmbientIntensity {
			get { 
				return AmbientIntensity;
			}
		}

		public float GetReflectionIntensity {
			get { 
				return ReflectionIntensity;
			}
		}

		protected int OrderQuantity = 1;

		protected float SingleOrderPrice = 0.0f;

		void Start () {
			Quantity.text = OrderQuantity.ToString ("D2");
			Initialize ();
		}

		protected virtual void Initialize () {
			UpdateSingleOrderPrice ();
			UpdateHDRI ();
		}

		public void UpdateHDRI () {
			HDRI_Updater.UpdateMap (HDRIMaterial, AmbientIntensity, ReflectionIntensity);
		}

		public void Close () {
			if (CartEditMode && OrderQuantity > 0) {
				AddToCart ();
			} else {
				Destroy (gameObject);
			}
		}

		public virtual void AddToCart () {
			CartEditMode = true;
			gameObject.SetActive (false);
		}

		public int AdjustOrderQuantity {
			set {
				OrderQuantity += value;

				if (OrderQuantity > 0) {
				} else {
					OrderQuantity = 0;
				}

				UpdateSingleOrderPrice ();

				Quantity.text = OrderQuantity.ToString ("D2");
			}
		}

		protected virtual void UpdateSingleOrderPrice () {
			PriceText.text = "RM " + (SingleOrderPrice * OrderQuantity).ToString ("F");
		}

		public float GetOrderCost {
			get {
				return SingleOrderPrice * OrderQuantity;
			}
		}

		public int GetOrderQuantity {
			get {
				return OrderQuantity;
			}
		}
	}
}
