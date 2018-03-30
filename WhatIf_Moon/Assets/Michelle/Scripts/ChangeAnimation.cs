using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ChangeAnimation : MonoBehaviour {


	//private bool isTouching = false;

	Animator myAnim;
	public GameObject Astronaut;
	public GameObject videoMoonLanding;

	void Start (){
		myAnim = Astronaut.GetComponent<Animator> ();
		videoMoonLanding.SetActive (false);
	}


	void sittingbaby(){
		myAnim.SetBool ("isSitting", true);
		myAnim.SetBool ("isStandingOn", false);
	}


	void standingbaby(){
		myAnim.SetBool ("isStandingOn", true);
		myAnim.SetBool ("isSitting", false);
	}


	void OnTriggerEnter(Collider other) {

		Debug.Log("touched!");
		standingbaby ();
		videoMoonLanding.SetActive (true);



	}

	void OnTriggerExit(Collider other){

		sittingbaby ();
		videoMoonLanding.SetActive (false);

	}
}