using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using BMJ.M360.Generic;

namespace BMJ.M360.MRCA.BaseSelectors {
	public class BaseSelectorDetector : InstancedMonoBehaviour<BaseSelectorDetector> {
		bool MouseOver = false;

		public static bool GetMouseOver {
			get {
				if (GetInstance == null) {
					print ("BaseSelectorDetector == null");
					return false;
				} else {
					print ("BaseSelectorDetector exists!");
					return GetInstance.MouseOver;
				}
			}
		}

		void OnMouseOver() {
			MouseOver = true;
		}

		void OnMouseExit() {
			MouseOver = false;
		}
	}
}
