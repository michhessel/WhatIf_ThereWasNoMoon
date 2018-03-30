using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;


public class PlayAstronaut : MonoBehaviour {

	//	public GameObject playButtonGO;
	//	public GameObject myCamera;

	public Button playButton;
	public Button backButton;
	public Button noMoon;

	public GameObject Moon;
	public GameObject Astronaut2;
	public GameObject OfficeDesk;
	public GameObject USAFlag;
	public GameObject MoonCut;

	Animator myAnim;


	// Use this for initialization
	void Start () {
		playButton.onClick.AddListener (AstronautSequence);
		backButton.onClick.AddListener (BackSequence);
		noMoon.onClick.AddListener (officeSequence);
		myAnim = Astronaut2.GetComponent<Animator> ();
	
	}



	void AstronautSequence (){
		Astronaut2.SetActive (true);
		Moon.SetActive (false);
		USAFlag.SetActive (true);
		MoonCut.SetActive (true);
		OfficeDesk.SetActive (false);

		myAnim.SetBool ("sit", false);
		myAnim.SetBool ("stand", true);
	}

	void BackSequence (){
		Moon.SetActive (true);
		Astronaut2.SetActive (false);
		OfficeDesk.SetActive (false);
		USAFlag.SetActive (false);
	}

	void officeSequence (){
		Astronaut2.SetActive (true);
		OfficeDesk.SetActive (true);
		Moon.SetActive (false);
		MoonCut.SetActive (false);
		USAFlag.SetActive (false);

		myAnim.SetBool ("sit", true);
		myAnim.SetBool ("stand", false);
	}





	//	void OnTriggerEnter(Collider other) {
	//		playButtonGO.SetActive (true);
	//
	//	}
	//
	//	void OnTriggerExit(Collider other) {
	//		playButtonGO.SetActive (false);
	//	}
}
