using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileMatchingManager : SingletonMonoBehaviourFast<TileMatchingManager> {
	private TileBehaviour[] tiles;
	private const float pixelByUnit = 100.0f;
	private const float tileSearchDistance = 400.0f / pixelByUnit; // double of tile size

	// Use this for initialization
	void Start () {
		tiles = this.gameObject.GetComponentsInChildren<TileBehaviour> ();
	}

	public bool ClearMatchingTiles(TileBehaviour tile) {
		IList matchedTiles = SearchNearestSameTiles (tile);

		if (matchedTiles.Count >= 2) {
			foreach (TileBehaviour matchedTile in matchedTiles) {
				matchedTile.UnSetElementImage ();
			}
			return true;
		}

		return false;
	}

	List<TileBehaviour> SearchNearestSameTiles(TileBehaviour tile) {
		int count = 0;
		List<TileBehaviour> list = new List<TileBehaviour> ();
		foreach (TileBehaviour checkTile in SearchTilesInRange (tile, tileSearchDistance)) {
			if (tile.IsSameElementImage(checkTile)) {
				count++;
				list.Add (checkTile);
			}
		}

		return list;
	}

	List<TileBehaviour> SearchTilesInRange(TileBehaviour tile, float distance) {
		List<TileBehaviour> list = new List<TileBehaviour> ();
		for (int i = 0; i < tiles.Length; i++) {
			if (Object.ReferenceEquals(tiles[i],tile)) { continue; }
			if (IsRangeOfDistance(tile.gameObject, tiles[i].gameObject, distance)) {
				list.Add(tiles[i]);
			}
		}
		return list;
	}

	bool IsRangeOfDistance(GameObject object1, GameObject object2, float maxDistance) {
		float dist = Vector3.Distance (object1.transform.position, object2.transform.position);
		return dist <= maxDistance;
	}
}
