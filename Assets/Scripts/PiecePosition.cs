using UnityEngine;

public struct PiecePosition {
	public PieceModelEnum ModelEnum;
	public Vector2Int pos;
	public Side facePos;

	public PiecePosition(PieceModelEnum modelEnum, Vector2Int pos, Side facePos) {
		this.ModelEnum = modelEnum;
		this.pos = pos;
		this.facePos = facePos;
	}
}