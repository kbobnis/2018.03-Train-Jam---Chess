using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pieces : MonoBehaviour {

	[SerializeField] private Piece piecePrefab;
	
	private List<Piece> pieces = new List<Piece>();

	private void Awake() {
		piecePrefab.gameObject.SetActive(false);
	}

	public void SpawnPieces(StartingPos startingPos) {
		foreach (Player player in startingPos.startingPos.Keys) {
			foreach (PiecePosition piecePos in startingPos.startingPos[player]) {
				Piece piece = SpawnPiece(piecePos);
				piece.SetPlayer(player);
				pieces.Add(piece);
			}
		}
	}

	private Piece SpawnPiece(PiecePosition piecePos) {
		Piece piece = Instantiate(piecePrefab, this.transform);
		piece.gameObject.SetActive(true);
		piece.transform.position = new Vector3(-piecePos.position.x, piecePrefab.transform.position.y, piecePos.position.y);
		piece.transform.rotation = Quaternion.AngleAxis(piecePos.rotation, new Vector3(0, 1, 0));
		piece.SetType(piecePos.type);
		return piece;
	}
}
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
	public static readonly StartingPos Chess = new StartingPos(new Dictionary<Player, List<PiecePosition>>() {
		{
			Player.Player1,
			new List<PiecePosition>() {
				new PiecePosition(PieceType.Pawn, new Position(0, 6), 0),
				new PiecePosition(PieceType.Pawn, new Position(1, 6), 0),
				new PiecePosition(PieceType.Pawn, new Position(2, 6), 0),
				new PiecePosition(PieceType.Pawn, new Position(3, 6), 0),
				new PiecePosition(PieceType.Pawn, new Position(4, 6), 0),
				new PiecePosition(PieceType.Pawn, new Position(5, 6), 0),
				new PiecePosition(PieceType.Pawn, new Position(6, 6), 0),
				new PiecePosition(PieceType.Pawn, new Position(7, 6), 0),
				
				new PiecePosition(PieceType.Rook, new Position(0, 7), 0),
				new PiecePosition(PieceType.Knight, new Position(1, 7), 0),
				new PiecePosition(PieceType.Bishop, new Position(2, 7), 0),
				new PiecePosition(PieceType.Queen, new Position(3, 7), 0),
				new PiecePosition(PieceType.King, new Position(4, 7), 0),
				new PiecePosition(PieceType.Bishop, new Position(5, 7), 0),
				new PiecePosition(PieceType.Knight, new Position(6, 7), 0),
				new PiecePosition(PieceType.Rook, new Position(7, 7), 0),
			}
		}, {
			Player.Player2,
			new List<PiecePosition>() {
				new PiecePosition(PieceType.Pawn, new Position(0, 1), 180),
				new PiecePosition(PieceType.Pawn, new Position(1, 1), 180),
				new PiecePosition(PieceType.Pawn, new Position(2, 1), 180),
				new PiecePosition(PieceType.Pawn, new Position(3, 1), 180),
				new PiecePosition(PieceType.Pawn, new Position(4, 1), 180),
				new PiecePosition(PieceType.Pawn, new Position(5, 1), 180),
				new PiecePosition(PieceType.Pawn, new Position(6, 1), 180),
				new PiecePosition(PieceType.Pawn, new Position(7, 1), 180),
				
				new PiecePosition(PieceType.Rook, new Position(0, 0), 180),
				new PiecePosition(PieceType.Knight, new Position(1, 0), 180),
				new PiecePosition(PieceType.Bishop, new Position(2, 0), 180),
				new PiecePosition(PieceType.Queen, new Position(3, 0), 180),
				new PiecePosition(PieceType.King, new Position(4, 0), 180),
				new PiecePosition(PieceType.Bishop, new Position(5, 0), 180),
				new PiecePosition(PieceType.Knight, new Position(6, 0), 180),
				new PiecePosition(PieceType.Rook, new Position(7, 0), 180),
			}
		}
	});

	public readonly Dictionary<Player, List<PiecePosition>> startingPos;

	private StartingPos(Dictionary<Player, List<PiecePosition>> dict) {
		startingPos = dict;
	}
}

public enum Player {
	Player1, Player2
}

public enum PieceType {
	Pawn, King, Queen, Knight, Bishop, Rook, Generic
}

public class Position {
	public readonly int x, y;

	public Position(int x, int y) {
		this.x = x;
		this.y = y;
	}
}