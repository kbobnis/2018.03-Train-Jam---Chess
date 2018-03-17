using UnityEngine;

public struct PiecePosition {
	public PieceModel model;
	public Vector2Int pos;
	public Side facePos;

	public PiecePosition(PieceModel model, Vector2Int pos, Side facePos) {
		this.model = model;
		this.pos = pos;
		this.facePos = facePos;
	}
}