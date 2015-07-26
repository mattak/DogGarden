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
		int rank = GetDogRankByRandom ();
		return GetDogSpriteByRank(rank);
	}

	public int GetDogRankByRandom() {
		int rankLimit = (currentMaxRank > 4) ? 4 : (currentMaxRank <= 0) ? 1 : currentMaxRank;
		int rand = Random.Range (0, rankLimit);
		Debug.Log ("generate by random : " + rand + " by " + rankLimit);
		return rand;
	}

	public bool UpdateMaxRank(int rank) {
		if (rank < 0) { return false; }
		this.currentMaxRank = rank;
		Debug.Log ("update max rank = " + currentMaxRank);
		return true;
	}
}
