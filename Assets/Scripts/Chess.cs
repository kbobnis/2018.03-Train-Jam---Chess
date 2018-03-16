using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chess : MonoBehaviour {

	[SerializeField] private Board board;

	[SerializeField] private Pieces pieces;
	
	// Use this for initialization
	void Start () {
		SpawnGame();
	}

	private void SpawnGame() {
		board.SpawnBoard(8, 8);

		pieces.SpawnPieces(StartingPos.Chess);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
