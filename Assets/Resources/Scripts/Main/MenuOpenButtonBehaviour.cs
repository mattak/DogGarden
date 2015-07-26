using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Collections;

public class MenuOpenButtonBehaviour : MonoBehaviour {
	public GameObject menuLayer;

	// Use this for initialization
	void Start () {
		this.GetComponent<Button>().OnClickAsObservable().Subscribe (_ => {
			menuLayer.SetActive (true);
		});
	}
}
