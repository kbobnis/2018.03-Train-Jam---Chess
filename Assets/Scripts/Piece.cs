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

	private Side facePos; 

	public Player owner;
	public bool isSelected { get; private set; }

	public void Init(PiecePosition piecePos, Player player, PieceMovement movement) {
		facePos = piecePos.facePos;
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

	public void MoveTo(Vector2Int pos, Action afterAction) {
		gameObject.AddComponent<Mover3d>().MoveTo(transform, new Vector3(-pos.x, 0, pos.y), () => {
			afterAction();
			this.pos.x = pos.x;
			this.pos.y = pos.y;
			if (OnFinishedMove != null) {
				OnFinishedMove(this);
			}
		}, Percent.One);
	}

	public void ToggleSelectable(bool b) {
		if (b) {
			GetComponent<ParticleSystem>().Play();
		} else {
			GetComponent<ParticleSystem>().Stop();
		}
	}

	public bool CanMoveTo(Vector2Int tilePos, List<Vector2Int> obstacles) {
		return movement.CanMoveTo(tilePos - pos, facePos, obstacles);
	}

	public bool CanAttack(Vector2Int tilePos, List<Vector2Int> obstacles) {
		return movement.CanAttack(tilePos - pos, facePos, obstacles);
	}
}

[Serializable]
public class PieceTypeAndMesh {
	public PieceModel Model;
	public Mesh mesh;
}