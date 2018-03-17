using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class PieceMovement {
	public abstract bool CanMoveTo(Vector2Int pos, Side wheresFace, List<Vector2Int> obstacles);
	public abstract bool CanAttack(Vector2Int pos, Side wheresFace, List<Vector2Int> obstacles);

	public class Knight : PieceMovement {
		private readonly Vector2Int[] moves = {
			new Vector2Int(-1, -2), new Vector2Int(1, -2),
			new Vector2Int(2, -1), new Vector2Int(2, 1),
			new Vector2Int(-1, 2), new Vector2Int(1, 2),
			new Vector2Int(-2, -1), new Vector2Int(-2, 1),
		};

		public override bool CanMoveTo(Vector2Int pos, Side wheresFace, List<Vector2Int> obstacles) {
			foreach (Vector2Int move in moves) {
				if (pos.x == move.x && pos.y == move.y) {
					return true;
				}
			}
			return false;
		}

		public override bool CanAttack(Vector2Int pos, Side wheresFace, List<Vector2Int> obstacles) {
			return CanMoveTo(pos, wheresFace, obstacles);
		}
	}

	public class Pawn : PieceMovement {
		public override bool CanMoveTo(Vector2Int pos, Side wheresFace, List<Vector2Int> obstacles) {
			if (wheresFace == Side.Up) {
				return pos.x == 0 && pos.y == -1;
			} else if (wheresFace == Side.Down) {
				return pos.x == 0 && pos.y == 1;
			} else {
				throw new NotImplementedException();
			}
		}

		public override bool CanAttack(Vector2Int pos, Side wheresFace, List<Vector2Int> obstacles) {
			if (wheresFace == Side.Up) {
				return (pos.x == -1 || pos.x == 1) && pos.y == -1;
			} else if(wheresFace == Side.Down) {
				return (pos.x == -1 || pos.x == 1) && pos.y == 1;
			} else {
				throw new NotImplementedException();
			}
		}
	}

	public class Bishop : PieceMovement {
		public override bool CanMoveTo(Vector2Int pos, Side wheresFace, List<Vector2Int> obstacles) {
			bool isOnLine = Math.Abs(pos.x) == Math.Abs(pos.y);
			if (isOnLine) {
				int signX = Math.Sign(pos.x);
				int signY = Math.Sign(pos.y);
				for (int x = 1; x < Math.Abs(pos.x); x++) {
					Vector2Int checkPos = new Vector2Int(x * signX, x * signY);
					if (obstacles.Contains(checkPos)) {
						return false;
					}
				}
				return true;
			}
			return false;
		}

		public override bool CanAttack(Vector2Int pos, Side wheresFace, List<Vector2Int> obstacles) {
			return CanMoveTo(pos, wheresFace, obstacles);
		}
	}

	public class King : PieceMovement {
		public override bool CanMoveTo(Vector2Int pos, Side wheresFace, List<Vector2Int> obstacles) {
			return Math.Abs(pos.x) <= 1 && Math.Abs(pos.y) <= 1;
		}

		public override bool CanAttack(Vector2Int pos, Side wheresFace, List<Vector2Int> obstacles) {
			return CanMoveTo(pos, wheresFace, obstacles);
		}
	}
}