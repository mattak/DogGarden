using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Collections;

public class ElementBehaviour : MonoBehaviour {
	public IntReactiveProperty rank = new IntReactiveProperty();
	private Image image;

	// Use this for initialization
	void Start () {
		image = this.GetComponent<Image> ();
		rank.Subscribe (value => {
			if (value >= 0) {
				this.image.sprite = ElementGeneratorBehaviour.Instance.GetDogSpriteByRank (value);
				this.image.color = Color.white;
			}
			else {
				this.image.sprite = null;
				this.image.color = Color.clear;
			}
		});

		Clear ();
	}
	
	public bool IsSameElementImage(ElementBehaviour target) {
		return image.sprite.Equals (target.image.sprite);
	}
	
	public bool IsEmpty() {
		return image.sprite == null;
	}

	public void SetRankByRandom() {
		this.rank.Value = ElementGeneratorBehaviour.Instance.GetDogRankByRandom ();
	}

	public void Clear() {
		this.rank.Value = -1;
	}
}