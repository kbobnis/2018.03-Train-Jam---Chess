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

	/// <summary>
	/// right now we create two players. one up and one down
	/// </summary>
	public List<Player> CreatePlayers(Material[] pieceMaterials) {
		return new List<Player>() {
			new Player(pieceMaterials[0], Side.Up),
			new Player(pieceMaterials[1], Side.Down),
		};
	}

	public PieceMovement GetMovesFor(PieceModelEnum modelEnum) {
		switch (modelEnum) {
			case PieceModelEnum.Pawn: return new PieceMovement.Pawn();
			case PieceModelEnum.Knight: return new PieceMovement.Knight();
			case PieceModelEnum.King: return new PieceMovement.King();
			case PieceModelEnum.Bishop: return new PieceMovement.Bishop();
			case PieceModelEnum.Queen: return new PieceMovement.Queen();
			case PieceModelEnum.Rook: return new PieceMovement.Rook();
			default:
				throw new NotImplementedException();
		}
	}
}