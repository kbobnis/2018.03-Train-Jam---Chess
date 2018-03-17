using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.U2D;

/// <summary>
/// 8x8 chess board
/// 0,0 1,0 2,0 3,0 4,0 5,0 6,0 7,0 
/// 0,1 1,1 2,1 3,1 4,1 5,1 6,1 7,1
/// 0,2 1,2 2,2 3,2 4,2 5,2 6,2 7,2
/// 0,3 1,3 2,3 3,3 4,3 5,3 6,3 7,3
/// 0,4 1,4 2,4 3,4 4,4 5,4 6,4 7,4
/// 0,5 1,5 2,5 3,5 4,5 5,5 6,5 7,5
/// 0,6 1,6 2,6 3,6 4,6 5,6 6,6 7,6
/// 0,7 1,7 2,7 3,7 4,7 5,7 6,7 7,7
/// </summary>

public class StartingPos {
	public static readonly StartingPos Chess = new StartingPos(new List<List<PiecePosition>>() {
		new List<PiecePosition>() {
			new PiecePosition(PieceModelEnum.Pawn, new Vector2Int(0, 6), Side.Up),
			new PiecePosition(PieceModelEnum.Pawn, new Vector2Int(1, 6), Side.Up),
			new PiecePosition(PieceModelEnum.Pawn, new Vector2Int(2, 6), Side.Up),
			new PiecePosition(PieceModelEnum.Pawn, new Vector2Int(3, 6), Side.Up),
			new PiecePosition(PieceModelEnum.Pawn, new Vector2Int(4, 6), Side.Up),
			new PiecePosition(PieceModelEnum.Pawn, new Vector2Int(5, 6), Side.Up),
			new PiecePosition(PieceModelEnum.Pawn, new Vector2Int(6, 6), Side.Up),
			new PiecePosition(PieceModelEnum.Pawn, new Vector2Int(7, 6), Side.Up),
				
			new PiecePosition(PieceModelEnum.Rook, new Vector2Int(0, 7), Side.Up),
			new PiecePosition(PieceModelEnum.Knight, new Vector2Int(1, 7), Side.Up),
			new PiecePosition(PieceModelEnum.Bishop, new Vector2Int(2, 7), Side.Up),
			new PiecePosition(PieceModelEnum.Queen, new Vector2Int(3, 7), Side.Up),
			new PiecePosition(PieceModelEnum.King, new Vector2Int(4, 7), Side.Up),
			new PiecePosition(PieceModelEnum.Bishop, new Vector2Int(5, 7), Side.Up),
			new PiecePosition(PieceModelEnum.Knight, new Vector2Int(6, 7), Side.Up),
			new PiecePosition(PieceModelEnum.Rook, new Vector2Int(7, 7), Side.Up),
		},
		new List<PiecePosition>() {
			new PiecePosition(PieceModelEnum.Pawn, new Vector2Int(0, 1), Side.Down),
			new PiecePosition(PieceModelEnum.Pawn, new Vector2Int(1, 1), Side.Down),
			new PiecePosition(PieceModelEnum.Pawn, new Vector2Int(2, 1), Side.Down),
			new PiecePosition(PieceModelEnum.Pawn, new Vector2Int(3, 1), Side.Down),
			new PiecePosition(PieceModelEnum.Pawn, new Vector2Int(4, 1), Side.Down),
			new PiecePosition(PieceModelEnum.Pawn, new Vector2Int(5, 1), Side.Down),
			new PiecePosition(PieceModelEnum.Pawn, new Vector2Int(6, 1), Side.Down),
			new PiecePosition(PieceModelEnum.Pawn, new Vector2Int(7, 1), Side.Down),
				
			new PiecePosition(PieceModelEnum.Rook, new Vector2Int(0, 0), Side.Down),
			new PiecePosition(PieceModelEnum.Knight, new Vector2Int(1, 0), Side.Down),
			new PiecePosition(PieceModelEnum.Bishop, new Vector2Int(2, 0), Side.Down),
			new PiecePosition(PieceModelEnum.Queen, new Vector2Int(3, 0), Side.Down),
			new PiecePosition(PieceModelEnum.King, new Vector2Int(4, 0), Side.Down),
			new PiecePosition(PieceModelEnum.Bishop, new Vector2Int(5, 0), Side.Down),
			new PiecePosition(PieceModelEnum.Knight, new Vector2Int(6, 0), Side.Down),
			new PiecePosition(PieceModelEnum.Rook, new Vector2Int(7, 0), Side.Down),
		}
	});

	public readonly List<List<PiecePosition>> startingPos;

	private StartingPos(List<List<PiecePosition>> dict) {
		startingPos = dict;
	}
}

public class Side {
	public static readonly Side Up = new Side(0);
	public static readonly Side Down = new Side(180);

	public readonly int angle;
	
	private Side(int angle) {
		this.angle = angle;
	}

	public float ToAngle() {
		return angle;
	}

	public Vector2Int Transform(Vector2Int pos) {
		if (angle != 180 && angle != 0) {
			throw new NotImplementedException();
		}
		if (angle == 180) {
			return  pos * -1;
		}
		return pos;
	}
}