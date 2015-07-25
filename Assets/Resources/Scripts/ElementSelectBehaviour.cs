using UnityEngine;
using System.Collections;
using UniRx.Triggers;
using UniRx;
using UnityEngine.UI;

public class ElementSelectBehaviour : SingletonMonoBehaviourFast<ElementSelectBehaviour> {
	public Button[] buttons;
	private Image[] images;
	private Image selectedImage = null;

	// Use this for initialization
	void Start () {
		// setup elementImages
		images = new Image[buttons.Length];
		for (int i = 0; i < buttons.Length; i++) {
			images[i] = buttons[i].GetComponent<Image>();
			Image elementImage = buttons[i].transform.GetChild(0).GetComponent<Image>();
			SetElementImageByRandom(elementImage);
		}

		buttons[0].OnClickAsObservable ()
			.Subscribe(thisUnit => {
				SetTileImageSelected(images, 0);
					selectedImage = buttons[0].transform.GetChild (0).GetComponent<Image>();
				});
		
		buttons[1].OnClickAsObservable ()
			.Subscribe(thisUnit => {
				SetTileImageSelected(images, 1);
				selectedImage = buttons[1].transform.GetChild (0).GetComponent<Image>();
			});

		
		buttons[2].OnClickAsObservable ()
			.Subscribe(thisUnit => {
				SetTileImageSelected(images, 2);
				selectedImage = buttons[2].transform.GetChild (0).GetComponent<Image>();
			});
	}

	void SetTileImageSelected(Image[] images, int selectedIndex) {
		for (int i = 0; i < buttons.Length; i++) {
			string imagePath = (i == selectedIndex) ? "Sprites/stage/tile" : "Sprites/stage/next_tile";
			images[i].sprite = Resources.Load (imagePath, typeof(Sprite)) as Sprite;
		}
	}

	void SetTileImageNotSelected(Image[] images) {
		for (int i = 0; i < buttons.Length; i++) {
			images[i].sprite = Resources.Load ("Sprites/stage/next_tile", typeof(Sprite)) as Sprite;
		}
	}

	void SetElementImageByRandom(Image image) {
		image.sprite = ElementGeneratorBehaviour.Instance.GetDogSpriteByRandom ();
	}

	public Image GetSelectedImage() {
		return selectedImage;
	}

	public void UpdateElement(Image elementImage) {
		SetElementImageByRandom (elementImage);
		SetTileImageNotSelected (images);
	}
}