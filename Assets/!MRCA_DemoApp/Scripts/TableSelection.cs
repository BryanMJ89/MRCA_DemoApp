using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using BMJ.M360.Generic;
using UnityEngine.UI;

namespace BMJ.M360.MRCA {
	public class TableSelection : InstancedMonoBehaviour<TableSelection> {
		[SerializeField] GameObject TableSelectionPanel;
		[SerializeField] Transform PaxButtonHolder;
		[SerializeField] Button StartOrderButton;

		int PaxCount = 0;

		public static int GetPaxCount {
			get {
				if (GetInstance == null) {
					Debug.LogError ("TableSelection Instance null!");
					return 0;
				} else {
					return GetInstance.PaxCount;
				}
			}
		}

		public bool ToggleTableSelectionPanel {
			set {
				TableSelectionPanel.SetActive (value);
			}
		}

		public Transform Select {
			set {
				StartOrderButton.interactable = true;

				foreach (Transform Child in PaxButtonHolder) {
					Child.Find ("ON").gameObject.SetActive (Child.Equals (value));
					Child.Find ("OFF").gameObject.SetActive (!Child.Equals (value));
				}
			}
		}
	}
}