using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileMatchingManager : SingletonMonoBehaviourFast<TileMatchingManager> {
	private TileBehaviour[] tiles;
	private const float pixelByUnit = 100.0f;
	private const float nearestTileDistance = 2.23606798f * 1.01f; // sqrt((100/pixelperunit)^2+(200/pixelperunit)^2) * <a little offset>

	// Use this for initialization
	void Start () {
		tiles = this.gameObject.GetComponentsInChildren<TileBehaviour> ();
	}

	public bool ClearMatchingTiles(TileBehaviour tile) {
		HashSet<TileBehaviour> matchedTiles = SearchNearestSameTiles (tile);

		if (matchedTiles.Count >= 2) {
			foreach (TileBehaviour matchedTile in matchedTiles) {
				matchedTile.UnSetElement ();
			}
			return true;
		}

		return false;
	}

	HashSet<TileBehaviour> SearchNearestSameTiles(TileBehaviour tile) {
		HashSet<TileBehaviour> sameTiles = SearchSameTiles (tile, new HashSet<TileBehaviour>(tiles));
		HashSet<TileBehaviour> resultTiles = new HashSet<TileBehaviour>();

		// FIXME: it's not optimized.
		foreach (TileBehaviour checkTile in sameTiles) {
			if (IsRangeOfDistance(tile.gameObject, checkTile.gameObject, nearestTileDistance)) {
				resultTiles.Add (checkTile);
				HashSet<TileBehaviour> nearTiles = SearchTilesInRange (checkTile, sameTiles, nearestTileDistance);
				resultTiles.UnionWith(nearTiles);
			}
		}

		return resultTiles;
	}

	HashSet<TileBehaviour> SearchTilesInRange(TileBehaviour tile, HashSet<TileBehaviour> tileSet, float distance) {
		HashSet<TileBehaviour> list = new HashSet<TileBehaviour> ();
		foreach (TileBehaviour checkTile in tileSet) {
			if (Object.ReferenceEquals(checkTile,tile)) { continue; }
			if (IsRangeOfDistance(tile.gameObject, checkTile.gameObject, distance)) {
				list.Add(checkTile);
			}
		}
		return list;
	}

	HashSet<TileBehaviour> SearchSameTiles(TileBehaviour tile, HashSet<TileBehaviour> tileSet) {
		HashSet<TileBehaviour> list = new HashSet<TileBehaviour>();
		foreach (TileBehaviour checkTile in tileSet) {
			if (Object.ReferenceEquals(checkTile,tile)) { continue; }
			if (tile.IsSameElementTile(checkTile)) {
				list.Add (checkTile);
			}
		}
		return list;
	}

	bool IsRangeOfDistance(GameObject object1, GameObject object2, float maxDistance) {
		float dist = Vector3.Distance (object1.transform.position, object2.transform.position);
		return dist <= maxDistance;
	}

	public bool IsFullTiles() {
		foreach (TileBehaviour tile in tiles) {
			if (tile.IsEmptyTile()) {
				return false;
			}
		}
		return true;
	}
}
