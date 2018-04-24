using UnityEngine;
using System.Collections;

public class ScoreController : Singleton<ScoreController> {


	public float rings;

	// Use this for initialization
	void Start () {
		rings = 0;
	}

	public void addRing(float ring){
		this.rings += ring;
	}
	public void Restart(){
		rings = 0;
	}
}
