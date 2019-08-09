using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace BMJ.M360.Generic {
	public class InstancedMonoBehaviour<T> : MonoBehaviour where T : InstancedMonoBehaviour<T> {
		static T Instance;

		public static T GetInstance {
			get { 
				return Instance;
			}
		}

		public static void DestroyInstance () {
			if (Instance == null) {
			} else {
				Destroy (Instance.gameObject.GetComponent<T> () as Object);

				if (Instance.gameObject.GetComponent<MonoBehaviour> () == null) {
					Destroy (Instance.gameObject);
				}

				Instance = null;
			}
		}

		protected void Awake () {
			if (Instance == null) {
				Instance = gameObject.GetComponent<T> ();
				OnAwake ();
			} else {
				print ("BaseSelectorDetector instance exists!");
				Destroy (gameObject.GetComponent<T> () as Object);

				if (gameObject.GetComponent<MonoBehaviour> () == null) {
					Destroy (gameObject);
				}
			}
		}

		protected virtual void OnAwake () {
			
		}

		void Destroy () {
			if (Instance.Equals (this)) {
				Instance = null;
			}

			OnDestroy ();
		}

		protected virtual void OnDestroy () {

		}
	}
}