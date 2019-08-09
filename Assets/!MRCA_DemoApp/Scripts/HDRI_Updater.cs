using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using BMJ.M360.Generic;

namespace BMJ.M360.MRCA {
	public class HDRI_Updater : InstancedMonoBehaviour<HDRI_Updater> {
		public static void UpdateMap (Material SetMap, float SetAmbientIntensity, float SetReflectionIntensity) {
			if (GetInstance == null) {
			} else {
				RenderSettings.skybox = SetMap;
				RenderSettings.ambientIntensity = SetAmbientIntensity + 2.0f;
				RenderSettings.reflectionIntensity = SetReflectionIntensity;
				#if UNITY_EDITOR
				DynamicGI.UpdateEnvironment ();
				#endif
			}
		}
	}
}