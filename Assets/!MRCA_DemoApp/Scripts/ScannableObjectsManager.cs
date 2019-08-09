using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using BMJ.M360.Generic;

namespace BMJ.M360.MRCA {
	public class ScannableObjectsManager : InstancedMonoBehaviour<ScannableObjectsManager> {
		[SerializeField] List<GameObject> ScannableObjects;

		public static List<GameObject> GetScannableObjects {
			get {
				if (GetInstance == null) {
					return new List<GameObject> ();
				} else {
					return GetInstance.ScannableObjects;
				}
			}	
		}
	}
}
