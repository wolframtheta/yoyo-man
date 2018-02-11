using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class behaviour : MonoBehaviour {

	// Use this for initialization

	Rigidbody2D rgbd;
	public GameObject yoyoPrefab;
	public Vector3 destinyDirection;
	public bool hooked = false;
	public bool shoot = false;
	public Vector3 destinityPosition;
	void Start () {
		rgbd = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		if (hooked){
			this.transform.position = Vector3.Lerp(destinityPosition,this.transform.position,0.75f);
		}
		if (Input.GetMouseButtonDown (0) && !shoot) {
			shoot = true;
			Vector3 worldTouch = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector2 dir = worldTouch - this.transform.position;
			GameObject yoyo = Instantiate (yoyoPrefab, this.transform.position, Quaternion.identity);
			yoyo.transform.SetParent (this.transform);
			Rigidbody2D yoyorgbd = yoyo.GetComponent<Rigidbody2D> ();
			yoyorgbd.velocity = 30.0f * ( Vector3.Normalize(30.0f*dir));
		}
		if (hooked && Vector3.Distance (this.transform.position, destinityPosition) < 0.5f) {
			returnYoyo ();
		}
}

	public void moveTowardsYoyo(Vector3 pos){
		Debug.Log ("impact");
		hooked = true;
		destinyDirection = (pos - this.transform.position).normalized;
		destinityPosition = pos;
	}

	public void returnYoyo(){
		shoot = false;
		hooked = false;
	}
}
