using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Collections;

public class GameOverButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<Button> ().OnClickAsObservable().Subscribe(_ => {
			Application.LoadLevel ("Top");
		});
	}
}