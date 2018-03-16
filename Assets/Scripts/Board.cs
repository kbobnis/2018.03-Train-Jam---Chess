using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    [SerializeField] private Tile tilePrefab;

    private List<Tile> tiles = new List<Tile>(); 

    public void SpawnBoard(int w, int h) {
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
                tile.SetColor(isWhite);
                isWhite = !isWhite;
            }
            isWhite = !isWhite;
        }
    }

    private Tile SpawnTile(int x, int y) {
        Tile tile = Instantiate(tilePrefab, gameObject.transform);
        tile.gameObject.SetActive(true);
        tile.gameObject.name = string.Format("Tile {0}, {1}", x, y);
        Vector3 scale = tile.gameObject.transform.localScale;
        tile.transform.position = new Vector3(-x, tilePrefab.transform.position.y, y);
        return tile;

    }

    // Update is called once per frame
    void Update() {
    }
}

