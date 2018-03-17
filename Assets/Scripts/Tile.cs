using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class Tile : GameElement {

	[SerializeField] private Material white, black, selected, canMove;

	public event Action<Tile> OnSelected;
	
	private bool isWhite;

	public Tile Init(Vector2Int pos) {
		gameObject.SetActive(true);
		name = string.Format("Tile {0}, {1}", pos.x, pos.y);
		this.pos = pos;
		this.isWhite = (pos.x + pos.y) % 2 == 0 ;
		RestoreMaterial();
		return this;
	}

	public void ShowPossibleMovement(bool canMoveTo) {
		if (canMoveTo) {
			GetComponent<MeshRenderer>().sharedMaterial = canMove;
		} else {
			RestoreMaterial();
		}
	}
	
	private void OnMouseDown() {
		if (OnSelected != null) {
			OnSelected(this);
		}
	}

	public void RestoreMaterial() {
		GetComponent<MeshRenderer>().sharedMaterial = isWhite ? white : black;
	}
}
