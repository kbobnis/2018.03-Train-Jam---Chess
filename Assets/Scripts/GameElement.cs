using System;
using UnityEngine;

public class GameElement : MonoBehaviour {
	
	public Vector2Int pos {
		get {
			Vector3 pos = transform.position;
			return new Vector2Int(-(int)Math.Round(pos.x), (int)Math.Round(pos.z));
		}
		protected set {
			transform.position = new Vector3(- value.x, 0, value.y);
		}
	}
	
}