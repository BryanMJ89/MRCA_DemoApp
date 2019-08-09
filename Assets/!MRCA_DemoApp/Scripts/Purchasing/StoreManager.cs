using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace BMJ.M360.Purchasing {
	public class StoreManager : MonoBehaviour {
		[SerializeField] Sprite DefaultPreviewSprite;

		string StoreLink = "http://192.168.0.122:8000/Documents/test_store_request.txt";

		List<StoreEntry> StoreEntries = new List<StoreEntry> ();

		bool StoreLoaded = false;

		public bool CheckStoreLoaded {
			get { 
				return StoreLoaded;
			}
		}

		void Start () {
			LoadStore ();
		}

		public void LoadStore () {
			StartCoroutine (LoadStoreRoutine ());
		}

		IEnumerator LoadStoreRoutine () {
			WWW LoadStoreRequest = new WWW (StoreLink);

			while (!LoadStoreRequest.isDone) {
				yield return null;
			}

			string ResponseText = LoadStoreRequest.text;

			StoreResponse Response = JsonUtility.FromJson<StoreResponse> (ResponseText);

			if (Response == null) {
				print ("Response unavailable.");
			} else {
				switch (Response.Result) {
				case 200:
					foreach (Purchasable PurchasableObject in Response.Store) {
						StartCoroutine (LoadStoreEntryRoutine (PurchasableObject));
					}
					break;
				default:
					print (ResponseText);
					break;
				}

				while (StoreEntries.Count < Response.Store.Count) {
					yield return null;
				}

				StoreLoaded = true;
			}
		}

		IEnumerator LoadStoreEntryRoutine (Purchasable PurchasableObject) {
			WWW LoadStoreEntryPreviewRequest = new WWW (PurchasableObject.PuchasePreviewLink);

			while (!LoadStoreEntryPreviewRequest.isDone) {
				yield return null;
			}

			Sprite PreviewImage = null;

			if (LoadStoreEntryPreviewRequest.texture == null) {
				PreviewImage = DefaultPreviewSprite;
			} else {
				PreviewImage = Sprite.Create (LoadStoreEntryPreviewRequest.texture, new Rect (0.0f, 0.0f, LoadStoreEntryPreviewRequest.texture.width, LoadStoreEntryPreviewRequest.texture.height), Vector2.one * 0.5f);
			}

			StoreEntries.Add (new StoreEntry (PurchasableObject.ID, PurchasableObject.Name, PreviewImage, PurchasableObject.Price, PurchasableObject.Description));
		}

		public StoreEntry GetStoreEntry (string ID) {
			return StoreEntries.Find (ParsedStoreEntry => ParsedStoreEntry.ID.Equals (ID));
		}
	}
}

