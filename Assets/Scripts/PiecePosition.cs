using UnityEngine;

public struct PiecePosition {
	public PieceModel model;
	public Vector2Int pos;
	public int rotation;

	public PiecePosition(PieceModel model, Vector2Int pos, int rotation) {
		this.model = model;
		this.pos = pos;
		this.rotation = rotation;
	}
}