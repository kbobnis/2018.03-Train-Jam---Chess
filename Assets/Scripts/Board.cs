using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
	[SerializeField] private Tile tilePrefab;

	public List<Tile> tiles = new List<Tile>();

	public List<Tile> SpawnBoard(int w, int h) {
		tilePrefab.gameObject.SetActive(true);
		foreach (Tile tile in tiles) {
			Destroy(tile.gameObject);
		}
		tiles.Clear();

		for (int x = 0; x < w; x++) {
			for (int y = 0; y < h; y++) {
				Tile tile = Instantiate(tilePrefab, gameObject.transform);
				tile.Init(new Vector2Int(x, y));
				tiles.Add(tile);
			}
		}
		return tiles;
	}

}