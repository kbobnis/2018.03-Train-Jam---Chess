using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
	[SerializeField] private Tile tilePrefab;

	public List<Tile> tiles = new List<Tile>();
	private int w, h;

	public List<Tile> SpawnBoard(int w, int h) {
		this.w = w;
		this.h = h;
		tilePrefab.gameObject.SetActive(true);
		foreach (Tile tile in tiles) {
			Destroy(tile.gameObject);
		}
		tiles.Clear();

		bool isWhite = false;
		for (int x = 0; x < w; x++) {
			for (int y = 0; y < h; y++) {
				Tile tile = SpawnTile(x, y);
				tiles.Add(tile);
				tile.Init(isWhite, new Vector2Int(x, y));
				isWhite = !isWhite;
			}
			isWhite = !isWhite;
		}
		return tiles;
	}

	private Tile SpawnTile(int x, int y) {
		Tile tile = Instantiate(tilePrefab, gameObject.transform);
		tile.gameObject.SetActive(true);
		tile.gameObject.name = string.Format("Tile {0}, {1}", x, y);
		Vector3 scale = tile.gameObject.transform.localScale;
		tile.transform.position = new Vector3(-x, tilePrefab.transform.position.y, y);
		return tile;
	}

	public Tile GetTileOn(Vector2Int pos) {
		return tiles[pos.x * w + pos.y];
	}
}