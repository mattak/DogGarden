using UnityEngine;
using System.Collections;

public class ElementGeneratorBehaviour : SingletonMonoBehaviourFast<ElementGeneratorBehaviour> {
	private string[] dogs = {
		"Sprites/dogs/white",
		"Sprites/dogs/cream",
		"Sprites/dogs/orange",
		"Sprites/dogs/red",
		"Sprites/dogs/pink",
		"Sprites/dogs/purple",
		"Sprites/dogs/skyblue",
		"Sprites/dogs/mint",
		"Sprites/dogs/navy", // TODO: change resource
	};
	private int currentMaxRank = 0;

	public void ResetRank() {
		currentMaxRank = 0;
	}

	public string GetDogSpritePathByRank(int rank) {
		if (rank >= dogs.Length || rank < 0) {
			return null;
		}
		return dogs[rank];
	}

	public Sprite GetDogSpriteByRank(int rank) {
		string spritePath = GetDogSpritePathByRank (rank);
		if (spritePath == null) {
			return null;
		}
		return Resources.Load<Sprite> (spritePath);
	}

	public Sprite GetDogSpriteByRandom() {
		int rankLimit = (currentMaxRank > 3) ? 3 : (currentMaxRank <= 0) ? 0 : currentMaxRank -1;
		int rank = Random.Range (0, rankLimit);
		return GetDogSpriteByRank(rank);
	}
}
