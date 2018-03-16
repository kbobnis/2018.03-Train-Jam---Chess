using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

	[SerializeField] private PieceTypeAndMesh[] meshes;
	[SerializeField] private PlayerAndMaterial[] materials ;
	
	private PieceType type;

	public void SetType(PieceType type) {
		this.type = type;
		Mesh foundMesh = null;
		foreach (PieceTypeAndMesh tuple in meshes) {
			if (tuple.type == type) {
				foundMesh = tuple.mesh;
			}
		}
		this.GetComponent<MeshFilter>().sharedMesh = foundMesh;
	}

	public void SetPlayer(Player player) {
		Material foundMat = null;
		foreach (PlayerAndMaterial tuple in materials) {
			if (tuple.player == player) {
				foundMat = tuple.material;
			}
		}
		gameObject.GetComponent<MeshRenderer>().sharedMaterial = foundMat;
	}
}

[Serializable]
public class PieceTypeAndMesh {
	public PieceType type;
	public Mesh mesh;
}

[Serializable]
public class PlayerAndMaterial {
	public Player player;
	public Material material;
}