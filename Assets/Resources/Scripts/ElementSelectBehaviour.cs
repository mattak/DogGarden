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
		images = new Image[buttons.Length];
		for (int i = 0; i < buttons.Length; i++) {
			images[i] = buttons[i].GetComponent<Image>() as Image;
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

	public Image GetSelectedImage() {
		return selectedImage;
	}

	public void UpdateElement(Image elementImage) {
		// TODO: change next image source.
		elementImage.sprite = Resources.Load<Sprite> ("Sprites/dogs/brown");
		SetTileImageNotSelected (images);
	}
}