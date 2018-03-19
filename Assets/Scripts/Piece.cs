using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Piece : GameElement {
	[SerializeField] private EnumToModel[] enumsToModels;
	[SerializeField] private Material selected, isInDanger;
	[SerializeField] private CinemachineVirtualCamera gameView, moveView;
	

	public event Action<Piece> OnSelected;
	public event Action<Piece> OnFinishedMove;
	public PieceModelEnum modelEnum;
	private GameObject model;

	public PieceMovement movement;

	private Side facePos;
	private bool neverMoved = true;

	public Player owner;
	public bool isSelected { get; private set; }

	public void Init(PiecePosition piecePos, Player player, PieceMovement movement) {
		gameObject.SetActive(true);
		transform.rotation = Quaternion.AngleAxis(piecePos.facePos.ToAngle(), new Vector3(0, 1, 0));
		facePos = piecePos.facePos;
		SetType(piecePos.ModelEnum);
		this.owner = player;
		gameObject.name = string.Format("{0} on {1},{2}", piecePos.ModelEnum, piecePos.pos.x, piecePos.pos.y);
		pos = piecePos.pos;
		this.movement = movement;
		ToggleSelect(false);
		model.AddComponent<Rotator>().enabled = false;
	}

	private void SetType(PieceModelEnum modelEnum) {
		this.modelEnum = modelEnum;
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
		moveView.LookAt = transform;
		moveView.gameObject.SetActive(true);
		
		gameObject.AddComponent<Mover3d>().MoveTo(transform, new Vector3(-pos.x, 0, pos.y), () => {
			moveView.gameObject.SetActive(false);
			afterAction();
			neverMoved = false;
			if (OnFinishedMove != null) {
				OnFinishedMove(this);
			}
			
		}, new Percent(0.03f));
	}

	public void ToggleSelectable(bool b) {
		model.GetComponent<Rotator>().enabled = b;
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