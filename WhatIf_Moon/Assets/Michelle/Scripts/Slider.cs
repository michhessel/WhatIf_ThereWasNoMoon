using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour {

	public float size;
	public GameObject moon;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void sizeChange(float newSize){
		size = newSize;
		moon.transform.localScale = new Vector3 (newSize, newSize, newSize);


	}
}
