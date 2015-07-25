using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System.Collections;

public class StartButtonBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<Button> ().OnClickAsObservable().Subscribe(_ => {
			Application.LoadLevel ("StageSelectMenu");
		});
	}
}
