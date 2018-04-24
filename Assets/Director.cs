using UnityEngine;
using System.Collections;

public class Director : System.Object {

	private static Director instance;
	public SceneController currentSceneController{ get; set;}

	public static Director getInstance(){
		if (instance == null) {
			instance = new Director ();
		}
		return instance;
	}
}
