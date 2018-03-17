using System;
using System.Collections.Generic;
using UnityEngine;

public class GameType {
	public static readonly GameType Chess = new GameType(8, 8, StartingPos.Chess);

	public readonly int w, h;
	public readonly StartingPos startingPos;

	private GameType(int w, int h, StartingPos startingPos) {
		this.w = w;
		this.h = h;
		this.startingPos = startingPos;
	}

	public List<Player> CreatePlayers(Material[] pieceMaterials) {
		int actualIndex = 0;
		List<Player> players = new List<Player>();
		foreach (List<PiecePosition> piecePositions in startingPos.startingPos) {
			players.Add(new Player(pieceMaterials[actualIndex++]));
		}
		return players;
	}

	public PieceMovement GetMovesFor(PieceModel model) {
		switch (model) {
			case PieceModel.Pawn: return new PieceMovement.Pawn();
			default:
			case PieceModel.Knight: return new PieceMovement.Knight();
		}
	}
}