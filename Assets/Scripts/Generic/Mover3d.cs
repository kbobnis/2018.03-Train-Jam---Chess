using System;
using UnityEngine;

public class Mover3d : MonoBehaviour {
	private Vector3 start = Vector3.zero;
	private Vector3 end;
	private Transform moveWhat;
	private float progress;
	private Action onFinish;
	private Percent speed;

	private void Update() {
		if (this.end != Vector3.zero) {
			float change = 0.1f / this.speed.value ;
			Vector3 delta = Vector3.Lerp(Vector3.zero, this.end - this.start, change);
			this.moveWhat.position = this.moveWhat.position + (Vector3)delta;
			this.progress += change;
			if (this.progress >= 1) {
				if (this.onFinish != null) {
					onFinish();
				}
				Destroy(this);
			}
		}
	}

	internal void MoveTo(Transform moveWhat, Vector3 @where, Action onFinish, Percent speed) {
		this.speed = speed;
		this.onFinish = onFinish;
		this.moveWhat = moveWhat;
		this.start = moveWhat.transform.position;
		this.end = @where;
	}
}
