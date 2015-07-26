using UnityEngine;
using System.Collections;

public class ResultScoreBehaviour : MonoBehaviour {
	void Start() {
		ScoreManager.Instance.score.Value = ScoreManager.Instance.LoadResultScore ();
	}
}