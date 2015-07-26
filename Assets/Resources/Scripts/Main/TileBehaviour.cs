using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;

public class TileBehaviour : MonoBehaviour {
	private Button button;
	private Image tileImage;
	private ElementBehaviour element;

	// Use this for initialization
	void Start () {
		button = this.gameObject.GetComponent<Button> ();
		tileImage = this.gameObject.GetComponent<Image> ();
		element = this.gameObject.transform.GetChild (0).GetComponent<ElementBehaviour>();

		button.OnClickAsObservable().Subscribe(_ => {
			PutElement (ElementSelectBehaviour.Instance.GetSelectedElement ());
		});
	}

	void PutElement(ElementBehaviour element) {
		if (element != null) {
			Debug.Log ("copyed rank is " + element.rank.Value);
			CopyElement (element);

			MatchAndJoinImages ();

			if (TileMatchingManager.Instance.IsFullTiles()) {
				Application.LoadLevel ("Over");
			}

			ElementSelectBehaviour.Instance.UpdateElements();
		}
	}

	void MatchAndJoinImages() {
		// delay
		if (TileMatchingManager.Instance.ClearMatchingTiles(this.GetComponent<TileBehaviour>())) {
			RankUpElement ();
			MatchAndJoinImages ();
		}
	}

	void RankUpElement() {
		this.element.rank.Value = element.rank.Value + 1;
		ScoreManager.Instance.AddScoreByRank(this.element.rank.Value);
		ElementGeneratorBehaviour.Instance.UpdateMaxRank (this.element.rank.Value);
	}

	void SetTileImage() {
		tileImage.sprite = Resources.Load<Sprite> ("Sprites/stage/tile");
		tileImage.color = Color.white;
	}

	void UnSetTileImage() {
		tileImage.sprite = null;
		tileImage.color = Color.clear;
	}

	void CopyElement (ElementBehaviour source) {
		this.element.rank.Value = source.rank.Value;
		ScoreManager.Instance.AddScoreByRank(this.element.rank.Value);
	}

	public void UnSetElement() {
		this.element.Clear ();
	}

	public bool IsSameElementTile(TileBehaviour tile) {
		return this.element.IsSameElementImage (tile.element);
	}

	public bool IsEmptyTile() {
		return this.element.IsEmpty ();
	}
}
