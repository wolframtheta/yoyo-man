using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yoyoBehaviour : MonoBehaviour {

	public float distance = 15f;
	public float velocity = 50f;
	public bool turned = false;

	private Rigidbody2D rgbd;
	private Vector3 origin;

	void Start(){
		rgbd = GetComponent<Rigidbody2D> ();
		origin = transform.parent.transform.position;
	}

	void Update(){
		if (!turned && Vector3.Distance (origin, this.transform.position) >= distance) {
			turned = true;
			rgbd.velocity = -rgbd.velocity;
		}
		if (turned && Vector3.Distance (origin, this.transform.position) < 0.5f) {
			this.transform.parent.SendMessage ("returnYoyo", this.transform.position);
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.transform.tag == "wall") {
			rgbd.velocity = Vector2.zero;
			this.transform.parent.SendMessage ("moveTowardsYoyo", this.transform.position);
		}
		else this.transform.parent.SendMessage ("returnYoyo", this.transform.position);
		Destroy (gameObject);
	}
		
}
