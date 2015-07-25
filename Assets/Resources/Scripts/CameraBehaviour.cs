using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {
	public Camera camera;
	
	// Use this for initialization
	void Awake() {
		float aspect = (float)Screen.height / Screen.width;
		float size = 640 * aspect / 100;
		Camera.main.orthographicSize = size;

		Vector3 position = Camera.main.transform.position;
		float heightOffset = Mathf.Floor((16.0f / 9.0f * Screen.width - Screen.height) / 2.0f);
		Vector3 screenWorldSize = Camera.main.ScreenToWorldPoint (new Vector3 (0, heightOffset, 0)) - Camera.main.ScreenToWorldPoint (Vector3.zero);
		float offset = screenWorldSize.y;
		Camera.main.transform.position = new Vector3 (position.x, position.y + offset, position.z);

		// Debug.Log ("aspect = " + aspect);
		// Debug.Log ("Screen.height = " + Screen.height);
		// Debug.Log ("orthographicSize = " + size);
		// Debug.Log ("16:9 " + 16.0f / 9.0f * Screen.width);
		// Debug.Log ("heightOffset = " + heightOffset);
		// Debug.Log ("offset = " + screenWorldSize);
	}
}