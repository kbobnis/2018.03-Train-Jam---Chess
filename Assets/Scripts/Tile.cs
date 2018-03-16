using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	[SerializeField] private Material white, black;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetColor(bool isWhite) {
		GetComponent<MeshRenderer>().sharedMaterial = isWhite ? white : black;
	}
}
