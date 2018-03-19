using UnityEngine;

public class Rotator : MonoBehaviour {

	[SerializeField] private float speed = 0.3f; 
	[SerializeField] private Vector3 axis = new Vector3(0, 1, 0); 
	
	void Update() {
		// myTransformGO.rotation.eulerAngles.x;
		Vector3 euler = gameObject.transform.rotation.eulerAngles;
		euler.y += speed * 360f * Time.deltaTime;
		gameObject.transform.rotation = Quaternion.Euler(euler);
		
		//gameObject.transform.Rotate(axis, 360f * speed * Time.deltaTime);
	}
	
}