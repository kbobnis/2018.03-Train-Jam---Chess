﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chess : MonoBehaviour {
	[SerializeField] private Board board;
	[SerializeField] private Pieces pieces;
	[SerializeField] private Material[] playerPiecesMaterials;

	private List<Player> players;
	private Player actualPlayer;
	private Player otherPlayer { get { return players[(players.IndexOf(actualPlayer) + 1) % players.Count]; }}

	private GameType gameType;

	void Start() {
		gameType = GameType.Chess;
		players = gameType.CreatePlayers(playerPiecesMaterials);

		SpawnGame(gameType, players);
		StartGame(gameType);
	}

	private void StartGame(GameType chess) {
		StartTurn(players[0]);
	}

	private void StartTurn(Player player) {
		actualPlayer = player;
		foreach (Piece piece in pieces.pieces) {
			piece.ToggleSelectable(piece.owner == player);
		}
	}

	private void SpawnGame(GameType game, List<Player> players) {
		List<Tile> tiles = board.SpawnBoard(game.w, game.h);

		foreach (Tile tile in tiles) {
			tile.OnSelected += OnTileSelected;
		}

		List<Piece> allPieces = pieces.SpawnPieces(game, players);

		foreach (Piece piece in allPieces) {
			piece.OnSelected += OnPieceSelected;
			piece.OnFinishedMove += OnPiecenFinishedMove;
		}
	}

	private void OnPiecenFinishedMove(Piece piece) {
		DeselectAll();
		StartTurn(otherPlayer);
	}

	private void DeselectAll() {
		foreach (Piece piece in pieces.pieces) {
			piece.ToggleSelect(false);
		}
		foreach (Tile tile in board.tiles) {
			tile.RestoreMaterial();
		}
	}

	private void OnTileSelected(Tile tile) {
		Piece pieceOn = pieces.GetPieceOn(tile.pos);
		if (pieceOn != null && pieceOn.owner == actualPlayer) {
			OnPieceSelected(pieceOn);
		} else {
			Piece piece = pieces.GetSelectedPiece();
			List<Tile> possibleTilesToMove = GetPossibleMovements(piece);
			if (possibleTilesToMove.Contains(tile)) {
				piece.MoveTo(tile.pos, () => { });
			} else {
				DeselectAll();
			}
		}
	}

	private void OnPieceSelected(Piece piece) {
		if (piece.owner == actualPlayer) {
			DeselectAll();
			piece.ToggleSelect(true);
			List<Tile> tiles = GetPossibleMovements(piece);
			foreach (Tile tile in tiles) {
				tile.ShowPossibleMovement(true);
				Piece pieceOn = pieces.GetPieceOn(tile.pos);
				if (pieceOn != null && IsOpponent(pieceOn.owner)) {
					pieceOn.IsInDanger();
				}
			}
		} else {
			Piece selected = pieces.GetSelectedPiece();
			if (selected != null) {
				List<Tile> attacks = GetPossibleAttacks(selected);
				foreach (Tile tileToAttack in attacks) {
					Piece pieceToAttack = pieces.GetPieceOn(tileToAttack.pos);
					if (pieceToAttack == piece) {
						selected.MoveTo(tileToAttack.pos, () => {
							pieces.RemoveFromPieces(pieceToAttack);
							actualPlayer.AddToGraveyard(pieceToAttack);
							if (piece.modelEnum == PieceModelEnum.King) {
								EndGame(otherPlayer);
							}
						});
						return;
					}
				}
			}
			DeselectAll();
		}
	}

	private void EndGame(Player winner) {
		SceneManager.LoadScene("SampleScene");
	}

	private bool IsOpponent(Player playerToCheck) {
		return actualPlayer != playerToCheck;
	}

	private List<Tile> GetPossibleAttacks(Piece piece) {
		List<Tile> whereToMove = new List<Tile>();
		List<Vector2Int> obstacles = pieces.RelativePositionsOfPieces(piece);
		//attacks (the same, but for pawn)
		foreach (Tile tile in board.tiles) {
			bool isAttackThere = piece.CanAttack(tile.pos, obstacles);
			Piece pieceOnTile = pieces.GetPieceOn(tile.pos);
			if (isAttackThere && (pieceOnTile != null && IsOpponent(pieceOnTile.owner))) {
				whereToMove.Add(tile);
			}
		}
		return whereToMove;
	}

	private List<Tile> GetPossibleMovements(Piece piece) {
		List<Tile> whereToMove = new List<Tile>();
		if (piece == null) {
			return whereToMove;
		}

		List<Vector2Int> obstacles = pieces.RelativePositionsOfPieces(piece);
		foreach (Tile tile in board.tiles) {
			bool isMovementThere = piece.CanMoveTo(tile.pos, obstacles);
			Piece pieceOnTile = pieces.GetPieceOn(tile.pos);
			if (isMovementThere && pieceOnTile == null) {
				whereToMove.Add(tile);
			}
		}
		whereToMove.AddRange(GetPossibleAttacks(piece));
		
		return whereToMove;
	}
}