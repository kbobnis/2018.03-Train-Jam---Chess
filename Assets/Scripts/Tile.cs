using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class Tile : MonoBehaviour {

	[SerializeField] private Material white, black, selected, canMove;

	public event Action<Tile> OnSelected;
	
	public Vector2Int pos;

	private bool isWhite;

	public void Init(bool isWhite, Vector2Int pos) {
		this.pos = pos;
		this.isWhite = isWhite;
		ToggleSelect(false);
	}

	public void ShowPossibleMovement(bool canMoveTo) {
		if (canMoveTo) {
			GetComponent<MeshRenderer>().sharedMaterial = canMove;
		} else {
			ToggleSelect(false);
		}
	}
	
	private void OnMouseDown() {
		if (OnSelected != null) {
			OnSelected(this);
		}
	}

	public void ToggleSelect(bool b) {
		GetComponent<MeshRenderer>().sharedMaterial = isWhite ? white : black;
	}
}
