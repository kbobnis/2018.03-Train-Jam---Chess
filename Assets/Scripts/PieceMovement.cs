﻿using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PieceMovement {
	public abstract bool CanMoveTo(Vector2Int pos, Side wheresFace, List<Vector2Int> obstacles, bool neverMoved);

	public virtual bool CanAttack(Vector2Int pos, Side wheresFace, List<Vector2Int> obstacles, bool neverMoved) {
		return CanMoveTo(pos, wheresFace, obstacles, neverMoved);
	}

	private static bool RookMove(Vector2Int pos, Side wheresFace, List<Vector2Int> obstacles) {
		bool isinLine = Math.Abs(pos.x) == 0 || Math.Abs(pos.y) == 0;
		if (isinLine) {
			int signX = Math.Sign(pos.x);
			int signY = Math.Sign(pos.y);
			for (int i = 1; i < Math.Max(Math.Abs(pos.x), Math.Abs(pos.y)); i++) {
				Vector2Int checkPos = new Vector2Int(i * signX, i * signY);
				if (obstacles.Contains(checkPos)) {
					return false;
				}
			}
			return true;
		}
		return false;
	}

	private static bool BishopMove(Vector2Int pos, Side wheresFace, List<Vector2Int> obstacles) {
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

	public class Knight : PieceMovement {
		private readonly Vector2Int[] moves = {
			new Vector2Int(-1, -2), new Vector2Int(1, -2),
			new Vector2Int(2, -1), new Vector2Int(2, 1),
			new Vector2Int(-1, 2), new Vector2Int(1, 2),
			new Vector2Int(-2, -1), new Vector2Int(-2, 1),
		};

		public override bool CanMoveTo(Vector2Int pos, Side wheresFace, List<Vector2Int> obstacles, bool neverMoved) {
			foreach (Vector2Int move in moves) {
				if (pos.x == move.x && pos.y == move.y) {
					return true;
				}
			}
			return false;
		}
	}

	public class Pawn : PieceMovement {
		public override bool CanMoveTo(Vector2Int pos, Side wheresFace, List<Vector2Int> obstacles, bool neverMoved) {
			if (wheresFace == Side.Up) {
				return pos.x == 0 && (pos.y == -1 || neverMoved && pos.y == -2);
			} else if (wheresFace == Side.Down) {
				return pos.x == 0 && (pos.y == 1 || neverMoved && pos.y == 2);
			} else {
				throw new NotImplementedException();
			}
		}

		public override bool CanAttack(Vector2Int pos, Side wheresFace, List<Vector2Int> obstacles, bool neverMoved) {
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
		public override bool CanMoveTo(Vector2Int pos, Side wheresFace, List<Vector2Int> obstacles, bool neverMoved) {
			return BishopMove(pos, wheresFace, obstacles);
		}
	}

	public class King : PieceMovement {
		public override bool CanMoveTo(Vector2Int pos, Side wheresFace, List<Vector2Int> obstacles, bool neverMoved) {
			bool canMoveRegular = Math.Abs(pos.x) <= 1 && Math.Abs(pos.y) <= 1;
			//TODO: check for the castling situation
			//- king and rook have to be in their original position 
			//- there has to be nothing in between them
			//- there has to be no dangerous field between them or on them
			
			return canMoveRegular;
		}
	}

	public class Queen : PieceMovement {
		public override bool CanMoveTo(Vector2Int pos, Side wheresFace, List<Vector2Int> obstacles, bool neverMoved) {
			bool rookMove = RookMove(pos, wheresFace, obstacles);
			bool bishopMove = BishopMove(pos, wheresFace, obstacles);
			return rookMove || bishopMove;
		}

	}

	public class Rook : PieceMovement {
		public override bool CanMoveTo(Vector2Int pos, Side wheresFace, List<Vector2Int> obstacles, bool neverMoved) {
			return RookMove(pos, wheresFace, obstacles);
		}

	}
}