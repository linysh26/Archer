using UnityEngine;
using System.Collections;

public class ArrowFlying : MonoBehaviour {

	public float speed;
	// Use this for initialization
	void Start () {
		speed = 10.0f;
	}

	void FixedUpdate(){
		if (!gameObject.GetComponent<Rigidbody> ().isKinematic) {
			this.gameObject.GetComponent<Rigidbody> ().velocity = this.gameObject.transform.TransformDirection (Vector3.forward * speed);
			this.gameObject.GetComponent<Rigidbody> ().AddForce (RoundController.Instance.sideWind * RoundController.Instance.power * 10.0f);
		}
		if (this.gameObject.transform.position.z > 20) {
			Archer.Instance.recollectArrow (this.gameObject);
		}
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.name [0] == 'a')
			return;
		Archer.Instance.canFire = true;
		gameObject.GetComponent<Rigidbody> ().isKinematic = true;
		gameObject.GetComponent<CapsuleCollider> ().isTrigger = true;
		gameObject.transform.parent = collision.gameObject.transform;
		Vector3 position = this.transform.position;
		float x = position.x;
		float y = position.y;
		ScoreController.Instance.addRing(11.0f - Mathf.Sqrt(x * x + (y - 1.0f) * (y - 1.0f)));
		RoundController.Instance.nextArrow ();
		RoundController.Instance.isFlying = false;
		Archer.Instance.canFire = true;
	}
}
