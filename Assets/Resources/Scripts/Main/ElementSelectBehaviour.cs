using UnityEngine;
using System.Collections;
using UniRx.Triggers;
using UniRx;
using UnityEngine.UI;

public class ElementSelectBehaviour : SingletonMonoBehaviourFast<ElementSelectBehaviour> {
	public Button[] buttons;
	private Image[] tileImages;
	private ElementBehaviour[] elements;
	private ElementBehaviour selectedElement = null;
	private int selectedIndex = -1;

	// Use this for initialization
	void Start () {
		// setup elementImages
		elements = new ElementBehaviour[buttons.Length];
		tileImages = new Image[buttons.Length];

		for (int i = 0; i < buttons.Length; i++) {
			tileImages[i] = buttons[i].GetComponent<Image>();
			elements[i] = buttons[i].transform.GetChild(0).GetComponent<ElementBehaviour>();
			elements[i].SetRankByRandom();
		}

		buttons[0].OnClickAsObservable ()
			.Subscribe(thisUnit => {
				SetTileImageSelected(tileImages, 0);
				selectedElement = elements[0];
				selectedIndex = 0;
			});
		
		buttons[1].OnClickAsObservable ()
			.Subscribe(thisUnit => {
				SetTileImageSelected(tileImages, 1);
				selectedElement = elements[1];
				selectedIndex = 1;
			});

		buttons[2].OnClickAsObservable ()
			.Subscribe(thisUnit => {
				SetTileImageSelected(tileImages, 2);
				selectedElement = elements[2];
				selectedIndex = 2;
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

	public void UpdateElements() {
		if (selectedIndex < 0 || selectedIndex >= buttons.Length) {
			SetTileImageNotSelected (tileImages);
			return;
		}

		elements[selectedIndex].SetRankByRandom ();
		SetTileImageSelected (tileImages, selectedIndex);
	}

	public ElementBehaviour GetSelectedElement() {
		return selectedElement;
	}
}