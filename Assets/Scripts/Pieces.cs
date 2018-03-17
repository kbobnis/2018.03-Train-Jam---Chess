using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pieces : MonoBehaviour {

	[SerializeField] private Piece piecePrefab;
	
	public readonly List<Piece> pieces = new List<Piece>();

	private void Awake() {
		piecePrefab.gameObject.SetActive(false);
	}

	public List<Piece> SpawnPieces(GameType game, List<Player> players) {
		int actualPlayerIndex = 0;
		foreach (List<PiecePosition> piecePositions in game.startingPos.startingPos) {
			Player actualPlayer = players[actualPlayerIndex++];
			foreach (PiecePosition piecePos in piecePositions) {
				Piece piece = SpawnPiece(piecePos, actualPlayer, game.GetMovesFor(piecePos.model));
				pieces.Add(piece);
			}
		}
		return pieces;
	}

	private Piece SpawnPiece(PiecePosition piecePos, Player owner, PieceMovement movement) {
		Piece piece = Instantiate(piecePrefab, this.transform);
		piece.gameObject.SetActive(true);
		piece.transform.position = new Vector3(-piecePos.pos.x, piecePrefab.transform.position.y, piecePos.pos.y);
		piece.transform.rotation = Quaternion.AngleAxis(piecePos.rotation, new Vector3(0, 1, 0));
		piece.Init(piecePos, owner, movement);
		return piece;
	}

	public Piece GetPieceOn(Vector2Int pos) {
		foreach (Piece piece in pieces) {
			if (piece.pos.x == pos.x && piece.pos.y == pos.y) {
				return piece;
			}
		}
		return null;
	}

	public Piece GetSelectedPiece() {
		foreach (Piece piece in pieces) {
			if (piece.isSelected) {
				return piece;
			}
		}
		return null;
	}
}