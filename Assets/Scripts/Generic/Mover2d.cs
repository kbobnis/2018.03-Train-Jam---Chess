using System;
using UnityEngine;

public class Mover2d : MonoBehaviour {
	private Vector2 start = Vector2.zero;
	private Vector2 end;
	private Transform moveWhat;
	private float progress;
	private Action onFinish;
	private Percent speed;

	private void Update() {
		if (this.end != Vector2.zero) {
			float change = 0.1f / this.speed.value ;
			Vector2 delta = Vector2.Lerp(Vector2.zero, this.end - this.start, change);
			this.moveWhat.position = this.moveWhat.position + (Vector3)delta;
			this.progress += change;
			if (this.progress >= 1) {
				if (this.onFinish != null) {
					onFinish();
				}
				Destroy(gameObject);
			}
		}
	}

	internal void MoveTo(Transform moveWhat, Vector2 @where, Action onFinish, Percent speed) {
		this.speed = speed;
		this.onFinish = onFinish;
		this.moveWhat = moveWhat;
		this.start = moveWhat.transform.position;
		this.end = @where;
	}
}
