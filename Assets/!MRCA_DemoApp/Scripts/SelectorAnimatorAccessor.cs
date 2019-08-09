using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace BMJ.M360.MRCA {
	public class SelectorAnimatorAccessor : MonoBehaviour {
		[SerializeField] Animator AttachedAnimator;

		public int ManualSelect {
			set {
				AttachedAnimator.SetInteger ("Index", value);
			}
		}
	}
}