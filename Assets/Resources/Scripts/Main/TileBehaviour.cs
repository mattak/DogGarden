using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;

public class TileBehaviour : MonoBehaviour {
	private Button button;
	private Image tileImage;
	private Image elementImage;
	private int rank;

	// Use this for initialization
	void Start () {
		button = this.gameObject.GetComponent<Button> ();
		tileImage = this.gameObject.GetComponent<Image> ();
		elementImage = this.gameObject.transform.GetChild (0).GetComponent<Image>();
		rank = 0;

		button.OnClickAsObservable().Subscribe(_ => {
			PutImage (ElementSelectBehaviour.Instance.GetSelectedImage ());
		});
	}

	void PutImage(Image selectedImage) {
		if (selectedImage != null) {
			ElementSelectBehaviour.Instance.UpdateElement (selectedImage);
			SetElementImage (selectedImage.sprite);

			MatchAndJoinImages ();

			if (TileMatchingManager.Instance.IsFullTiles()) {
				Application.LoadLevel ("Over");
			}
		}
	}

	void MatchAndJoinImages() {
		// delay
		if (TileMatchingManager.Instance.ClearMatchingTiles(this.GetComponent<TileBehaviour>())) {
			UpdateRank ();
			MatchAndJoinImages ();
		}
	}

	void UpdateRank() {
		this.rank = this.rank + 1;
		Sprite sprite = ElementGeneratorBehaviour.Instance.GetDogSpriteByRank(this.rank);
		SetElementImage (sprite);
	}

	void SetElementImage(Sprite sprite) {
		elementImage.sprite = sprite;
		elementImage.color = Color.white;
	}

	public void UnSetElementImage() {
		this.rank = 0;
		elementImage.sprite = null;
		elementImage.color = Color.clear;
	}

	void SetTileImage() {
		tileImage.sprite = Resources.Load<Sprite> ("Sprites/stage/tile");
		tileImage.color = Color.white;
	}

	void UnSetTileImage() {
		tileImage.sprite = null;
		tileImage.color = Color.clear;
	}

	public bool IsSameElementImage(TileBehaviour target) {
		return elementImage.sprite.Equals (target.elementImage.sprite);
	}

	public bool IsEmptyTile() {
		return elementImage.sprite == null;
	}
}
