using UnityEngine;
using System.Collections;



/**
 * Since I was going to let the very object to care about its own deal, so I let the object's controller communicate with the 
 * 
 * action manager instead dealing cases in the SceneController
 * */


public class FirstController : MonoBehaviour, SceneController{

	Director director;

	public bool status;

	void Awake(){
		director = Director.getInstance ();
		director.currentSceneController = this;
		director.currentSceneController.LoadResources ();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		status = true;
	}

	void OnGUI(){

		GUI.Box (new Rect (10, 10, 100, 80), "Menu");

		if (GUI.Button (new Rect (30, 30, 60, 40), "Restart")) {
			Restart ();
		}
	}

	void Update(){
		if (status && Input.GetKeyDown(KeyCode.BackQuote)) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			status = false;
		} else if(Input.GetKeyDown(KeyCode.BackQuote)){
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			status = true;
		}
	}

	public void LoadResources(){
		Archer initial_p = Archer.Instance;
		ScoreController initial_s = ScoreController.Instance;
		RoundController initial_r = RoundController.Instance;
	}
	public void Restart(){
		RoundController.Instance.Restart ();
		ScoreController.Instance.Restart ();
		Archer.Instance.Restart ();
		status = true;
	}
}
