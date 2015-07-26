using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Collections;

public class MenuCloseButtonBehaviour : MonoBehaviour {
	public GameObject hideLayer;

	// Use this for initialization
	void Start () {
		this.GetComponent<Button> ().OnClickAsObservable().Subscribe(_ => {
			hideLayer.SetActive (false);
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
