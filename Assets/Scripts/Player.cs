using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;

public class Player {
	
	public Material piecesMaterial;
	public readonly Side side;
	
	List<Piece> graveyard = new List<Piece>(); 

	public Player(Material pieceMaterial, Side side) {
		this.piecesMaterial = pieceMaterial;
		this.side = side;
	}

	public void AddToGraveyard(Piece piece) {
		Vector3 whereToPutIt = new Vector3(-graveyard.Count, 0, -1);
		if (side == Side.Down) {
			whereToPutIt.z = 8;
		}
		piece.gameObject.AddComponent<Mover3d>().MoveTo(piece.transform, whereToPutIt, () => {
		}, Percent.One);
		graveyard.Add(piece);
	}
}