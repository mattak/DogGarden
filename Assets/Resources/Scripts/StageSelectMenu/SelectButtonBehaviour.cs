using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Collections;

public class SelectButtonBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<Button>().OnClickAsObservable().Subscribe(_ => {
			Application.LoadLevel ("Main");
		});
	}
}
