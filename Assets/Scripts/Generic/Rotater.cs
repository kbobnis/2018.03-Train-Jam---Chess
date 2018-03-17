using UnityEngine;

public class Rotater : MonoBehaviour {

	[SerializeField] private float speed = 0.5f; 
	[SerializeField] private Vector3 axis = new Vector3(0, 1, 0); 
	
	void Update() {
		gameObject.transform.Rotate(axis, 360f * speed * Time.deltaTime);
	}
	
}