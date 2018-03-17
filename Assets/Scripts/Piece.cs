using System;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {
	
	[SerializeField] private PieceTypeAndMesh[] meshes;
	[SerializeField] private Material selected, isInDanger;

	public event Action<Piece> OnSelected;
	public event Action<Piece> OnFinishedMove;
	public PieceModel model { get; private set; }
	public Vector2Int pos;
	public PieceMovement movement;
	public int rotation;

	public Player owner;
	public bool isSelected { get; private set; }

	public void Init(PiecePosition piecePos, Player player, PieceMovement movement) {
		this.rotation = piecePos.rotation;
		SetType(piecePos.model);
		gameObject.GetComponent<MeshRenderer>().sharedMaterial = player.piecesMaterial;
		this.owner = player;
		gameObject.name = string.Format("{0} on {1},{2}", piecePos.model, piecePos.pos.x, piecePos.pos.y);
		pos = piecePos.pos;
		this.movement = movement;
		GetComponent<ParticleSystem>().Pause();
	}
	
	private void SetType(PieceModel model) {
		this.model = model;
		Mesh foundMesh = null;
		foreach (PieceTypeAndMesh tuple in meshes) {
			if (tuple.Model == model) {
				foundMesh = tuple.mesh;
			}
		}
		this.GetComponent<MeshFilter>().sharedMesh = foundMesh;
		GetComponent<MeshCollider>().sharedMesh = foundMesh;
	}

	private void OnMouseDown() {
		if (OnSelected != null) {
			OnSelected(this);
		}
	}
	
	public void IsInDanger() {
		GetComponent<MeshRenderer>().sharedMaterial = isInDanger;
	}

	public void ToggleSelect(bool isSelected) {
		this.isSelected = isSelected;
		GetComponent<MeshRenderer>().sharedMaterial = isSelected ? selected : owner.piecesMaterial;
	}

	public void MoveTo(Tile tile) {
		gameObject.AddComponent<Mover3d>().MoveTo(transform, new Vector3(-tile.pos.x, 0, tile.pos.y), () => {
			pos.x = tile.pos.x;
			pos.y = tile.pos.y;
			if (OnFinishedMove != null) {
				OnFinishedMove(this);
			}
		}, Percent.One );
	}

	public void ToggleSelectable(bool b) {
		if (b) {
			GetComponent<ParticleSystem>().Play();
		}
		else {
			GetComponent<ParticleSystem>().Stop();
		}
	}
}

[Serializable]
public class PieceTypeAndMesh {
	public PieceModel Model;
	public Mesh mesh;
}