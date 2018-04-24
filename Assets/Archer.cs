using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Archer : Singleton<Archer> {

	public float next_fire;
	public float fire_rate;
	public bool canFire;

	public int restOfArrows;
	GameObject[] arrows;

	public float speedX;
	public float speedY;
	public float X;
	public float Y;

	FirstController scene_controller;

	public Archer(){}

	// Use this for initialization
	void Start () {
		scene_controller = (FirstController)Director.getInstance ().currentSceneController;
		canFire = true;
		next_fire = 0;
		fire_rate = 200;
		speedX = 200;
		speedY = 200;
		X = 0;
		Y = 0;
		restOfArrows = 10;
		arrows = new GameObject[10];
		for (int i = 0; i < 10; i++) {
			arrows [i] = Instantiate (Resources.Load ("arrow")) as GameObject;
			arrows [i].GetComponent<Rigidbody> ().isKinematic = true;
			arrows [i].GetComponent<CapsuleCollider> ().isTrigger = true;
			arrows [i].transform.position = this.transform.position;
		}
	}

	void Update(){
		if (scene_controller.status) {
			float translationX = Input.GetAxis ("Mouse X") * speedX;
			float translationY = Input.GetAxis ("Mouse Y") * speedY;
			translationX *= Time.deltaTime;
			translationY *= Time.deltaTime;
			X += translationX;
			Y += translationY;
			this.transform.rotation = Quaternion.identity;
			this.transform.RotateAround (this.transform.position, Vector3.right, -Y);
			this.transform.RotateAround (this.transform.position, Vector3.up, X);
		}
		if (Input.GetMouseButtonDown (0) && canFire && scene_controller.status ) {
			shoot ();
			canFire = false;
		}
	}

	void OnGUI(){
		GUI.Label (new Rect (Screen.width / 2 + 18, Screen.height / 2 - 15, 30, 20), "—");
		GUI.Label (new Rect (Screen.width / 2 - 32, Screen.height / 2 - 15, 30, 20), "—");
		GUI.Label (new Rect (Screen.width / 2 - 2, Screen.height / 2 + 10, 5, 30), "|");
		GUI.Label (new Rect (Screen.width / 2 - 2, Screen.height / 2 - 40, 5, 30), "|");
		GUI.Box (new Rect (Screen.width - 120, 10, 120, 100), "Rings: " + ScoreController.instance.rings);
		GUI.Label (new Rect (Screen.width - 100, 40, 80, 20), "Rest: " + restOfArrows);
	}

	public void shoot(){
		GameObject arrow = this.arrows [restOfArrows - 1];
		arrow.transform.position = this.transform.position;
		arrow.transform.rotation = this.transform.rotation;
		arrow.SetActive (true);
		arrow.GetComponent<Rigidbody> ().isKinematic = false;
		arrow.GetComponent<CapsuleCollider> ().isTrigger = false;
		arrow.GetComponent<Rigidbody> ().AddForce (RoundController.instance.sideWind * RoundController.instance.power * 10.0f);
		RoundController.instance.isFlying = true;
	}

	public void recollectArrow(GameObject arrow){
		arrow.GetComponent<Rigidbody>().isKinematic = true;
		arrow.transform.position = this.transform.position;
		arrow.transform.parent = this.transform;
		arrow.SetActive (false);
		RoundController.instance.isFlying = false;
		canFire = true;
	}
	public void recollectArrow(){
		for (; restOfArrows < 10; restOfArrows++) {
			arrows [restOfArrows].transform.position = this.transform.position;
			arrows [restOfArrows].transform.parent = this.transform;
			arrows [restOfArrows].SetActive (false);
		}
	}

	public void Restart(){
		scene_controller = (FirstController)Director.getInstance ().currentSceneController;
		canFire = true;
		speedX = 200;
		speedY = 200;
		X = 0;
		Y = 0;
		recollectArrow ();
	}
}
