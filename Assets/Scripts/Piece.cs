using System;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {
	[SerializeField] private EnumToModel[] enumsToModels;
	[SerializeField] private Material selected, isInDanger;

	public event Action<Piece> OnSelected;
	public event Action<Piece> OnFinishedMove;
	private PieceModelEnum ModelEnum;
	private GameObject model;
	public Vector2Int pos;
	public PieceMovement movement;

	private Side facePos;
	private bool neverMoved = true;

	public Player owner;
	public bool isSelected { get; private set; }

	public void Init(PiecePosition piecePos, Player player, PieceMovement movement) {
		facePos = piecePos.facePos;
		SetType(piecePos.ModelEnum);
		this.owner = player;
		gameObject.name = string.Format("{0} on {1},{2}", piecePos.ModelEnum, piecePos.pos.x, piecePos.pos.y);
		pos = piecePos.pos;
		this.movement = movement;
		GetComponent<ParticleSystem>().Pause();
	}

	private void SetType(PieceModelEnum modelEnum) {
		this.ModelEnum = modelEnum;
		GameObject prefab = null;
		
		foreach (EnumToModel enumsToModel in enumsToModels) {
			enumsToModel.model.SetActive(false);
		}
		
		foreach (EnumToModel enumToModel in enumsToModels) {
			if (enumToModel.@enum == modelEnum) {
				enumToModel.model.SetActive(true);
				model = enumToModel.model;
			}
		}
	}

	private void OnMouseDown() {
		if (OnSelected != null) {
			OnSelected(this);
		}
	}

	public void IsInDanger() {
		model.GetComponent<MeshRenderer>().sharedMaterial = isInDanger;
	}

	public void ToggleSelect(bool isSelected) {
		this.isSelected = isSelected;
		model.GetComponent<MeshRenderer>().sharedMaterial = isSelected ? selected : owner.piecesMaterial;
	}

	public void MoveTo(Vector2Int pos, Action afterAction) {
		gameObject.AddComponent<Mover3d>().MoveTo(transform, new Vector3(-pos.x, 0, pos.y), () => {
			afterAction();
			neverMoved = false;
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
		return movement.CanMoveTo(tilePos - pos, facePos, obstacles, neverMoved);
	}

	public bool CanAttack(Vector2Int tilePos, List<Vector2Int> obstacles) {
		return movement.CanAttack(tilePos - pos, facePos, obstacles, neverMoved);
	}
}

[Serializable]
public class EnumToModel {
	public PieceModelEnum @enum;
	public GameObject model;
}