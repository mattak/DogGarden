using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;

public class TileBehaviour : MonoBehaviour {
	private Button button;
	private Image tileImage;
	private Image elementImage;

	// Use this for initialization
	void Start () {
		button = this.gameObject.GetComponent<Button> ();
		tileImage = this.gameObject.GetComponent<Image> ();
		elementImage = this.gameObject.transform.GetChild (0).GetComponent<Image>();

		button.OnClickAsObservable().Subscribe(_ => {
			Image selectedImage = ElementSelectBehaviour.Instance.GetSelectedImage ();

			SetElementImage (selectedImage);
			ElementSelectBehaviour.Instance.UpdateElement (selectedImage);
		});
	}

	void SetElementImage(Image selectedImage) {
		elementImage.sprite = selectedImage.sprite;
		elementImage.color = Color.white;
	}

	void UnSetElementImage() {
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
}
