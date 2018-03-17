using UnityEngine;

public abstract class PieceMovement {

	public abstract bool CanMoveTo(Vector2Int pos);

	public class Knight : PieceMovement {

		private Vector2Int[] moves = new Vector2Int[] {
			new Vector2Int(-1, -2), new Vector2Int(1, -2),
			new Vector2Int(2, -1), new Vector2Int(2, 1),
			new Vector2Int(-1, 2), new Vector2Int(1, 2),
			new Vector2Int(-2, -1), new Vector2Int(-2, 1),
		};

		public override bool CanMoveTo(Vector2Int pos) {
			foreach (Vector2Int move in moves) {
				if (pos.x == move.x && pos.y == move.y) {
					return true;
				}
			}
			return false;
		}
	}

	public class Pawn : PieceMovement {
		public override bool CanMoveTo(Vector2Int pos) {
			return pos.x == 0 && pos.y == -1;
		}
	}
}

