using UnityEngine;
using System.Collections;

public class RoundController : Singleton<RoundController> {


	public int current_round { set; get; }
	public int rest { set; get; }
	public float timeOfTurn;
	public Vector3 sideWind;
	public float power;
	public bool isFlying;

	void Start(){
		this.rest = 10;
		this.current_round = 1;
		timeOfTurn = 30;
		isFlying = false;
		float x = Random.Range (-1.0f, 1.0f);
		float y = Mathf.Sqrt(1 - x * x);
		sideWind = new Vector3 (x, y, 0);
		power = Random.Range (1.0f, 12.0f);
	}

	void Update(){
		if (timeOfTurn == 0)
			nextArrow ();
		
		if (rest == 0)
			nextRound ();
		
		if(!isFlying)
			timeOfTurn -= Time.deltaTime > timeOfTurn?timeOfTurn:Time.deltaTime;
	}
	// Update is called once per frame

	void OnGUI(){
		GUI.Label (new Rect (Screen.width/2 - 20, 0, 60, 20), "Round: " + current_round);
		GUI.Label (new Rect (Screen.width / 2 - 20, 20, 40, 20), "" + timeOfTurn);
		GUI.Label (new Rect (Screen.width / 2 - 20, 40, 200, 20), "Direction: " + (sideWind.x > 0?"→":"←") + " " + (int)(sideWind.x * 10) + " " + (sideWind.y > 0?"↑":"↓") + " " + (int)(sideWind.y * 10));
		GUI.Label (new Rect (Screen.width / 2 - 20, 60, 60, 20), "level: " + power);
	}

	public void nextArrow(){
		rest--;
		timeOfTurn = 30;
		Archer.instance.restOfArrows--;
		//sidewind initialize
		float x = Random.Range (-1.0f, 1.0f);
		float y = Mathf.Sqrt(1 - x * x);
		sideWind = new Vector3 (x, y, 0);
		power = Random.Range (1.0f, 12.0f);
	}

	public void nextRound(){
		rest = 10;
		timeOfTurn = 30;
		Archer.instance.recollectArrow ();
	}

	public void Restart(){
		this.current_round = 1;
		this.rest = 10;
		timeOfTurn = 30;
	}
}
