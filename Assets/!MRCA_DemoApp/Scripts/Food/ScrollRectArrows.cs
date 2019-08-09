using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace BMJ.M360.Generic {
	[RequireComponent (typeof (ScrollRect))]
	public class ScrollRectArrows : MonoBehaviour {
		[SerializeField] GameObject UpArrow;
		[SerializeField] GameObject DownArrow; 

		ScrollRect AttachedScrollRect;

		void Start () {
			AttachedScrollRect = GetComponent<ScrollRect> ();
		}

		void Update () {
			if (AttachedScrollRect.content.rect.height < AttachedScrollRect.viewport.rect.height) {
				UpArrow.SetActive (false);
				DownArrow.SetActive (false);
			} else {
				UpArrow.SetActive (AttachedScrollRect.normalizedPosition.y < 0.9f);
				DownArrow.SetActive (AttachedScrollRect.normalizedPosition.y > 0.1f);
			}
		}
	}
}