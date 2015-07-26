using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Collections;

public class ScoreManager : SingletonMonoBehaviourFast<ScoreManager> {
	public LongReactiveProperty score = new LongReactiveProperty();
	public Image[] scores;

	// Use this for initialization
	void Start () {
		score.Subscribe (value => {
			SetScore (value);
		});
	}

	void SetScore(long value) {
		long limitScore = (long)Mathf.Pow (10, scores.Length) - 1; // 99999999

		// limit max
		if (limitScore <= value) {
			value = limitScore;
		}

		for (int i=0; i<scores.Length; i++) {
			bool empty = value <= 0 && i > 0;
			int digit = (int)(value % 10);
			value = value / 10;
			SetDigitImage(scores [i], digit, empty);
		}
	}

	void SetDigitImage (Image image, int value, bool emptyWhenZero) {
		Sprite sprite = GetDigitSprite (value, emptyWhenZero);
		if (sprite != null) {
			image.sprite = sprite;
			image.color = Color.white;
		} else {
			image.sprite = sprite;
			image.color = Color.clear;
		}
	}
	
	Sprite GetDigitSprite(int value, bool emptyWhenZero) {
		if (emptyWhenZero && value == 0) { return null; }
		return Resources.Load<Sprite>("Sprites/digit/" + value);
	}
}