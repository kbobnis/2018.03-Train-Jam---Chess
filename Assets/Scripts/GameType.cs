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

	public PieceMovement GetMovesFor(PieceModel model) {
		switch (model) {
			case PieceModel.Pawn: return new PieceMovement.Pawn();
			case PieceModel.Knight: return new PieceMovement.Knight();
				default:
			case PieceModel.Bishop: return new PieceMovement.Bishop();
		}
	}
}