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
	//0.5,-0.5  0.5,0  0.5,0.5
	//0,-0.5    0,0    0,0.5
	//-0.5,-0.5 -0.5,0 -0.5,0.5  
	// Update is called once per frame


	/*
	 * if (hit1.transform.gameObject != null && hit2.transform.gameObject != null && hit3.transform.gameObject != null && hit4.transform.gameObject != null 
				&& hit5.transform.gameObject != null && hit6.transform.gameObject != null && hit7.transform.gameObject != null && hit8.transform.gameObject != null 
				&& hit1.transform.gameObject == hit2.transform.gameObject && hit1.transform.gameObject == hit3.transform.gameObject && hit1.transform.gameObject == hit4.transform.gameObject
				&& hit1.transform.gameObject == hit5.transform.gameObject && hit1.transform.gameObject == hit6.transform.gameObject && hit1.transform.gameObject == hit7.transform.gameObject
				&& hit1.transform.gameObject == hit8.transform.gameObject) {
	 * 
	 * */
	void Update () {
		if (hooked){
//			Vector3 vel = destinyDirection  * 300.0f * Time.deltaTime;
//			rgbd.velocity = new Vector2 (vel.x,vel.y);
			this.transform.position = Vector3.Lerp(destinityPosition,this.transform.position,0.75f);
		}
//		if (Input.GetMouseButtonDown(0) && !hooked) {
//			Vector2 worldTouch = Camera.main.ScreenToWorldPoint (Input.mousePosition);
//			Vector2 dir = worldTouch - new Vector2 (this.transform.position.x, this.transform.position.y);
//			RaycastHit2D hit0 = Physics2D.Raycast (new Vector2 (this.transform.position.x, this.transform.position.y), dir, Mathf.Infinity);
//			if (hit0.transform.gameObject != null) {
//				Vector2 pos = new Vector2 (this.transform.position.x - 0.5f, this.transform.position.y - 0.5f);
//				RaycastHit2D hit1 = Physics2D.Raycast (pos , new Vector2(hit0.transform.gameObject.transform.position.x - pos.x, hit0.transform.gameObject.transform.position.y - pos.y), Mathf.Infinity);
//				pos = new Vector2 (this.transform.position.x, this.transform.position.y - 0.5f);
//				RaycastHit2D hit2 = Physics2D.Raycast (pos, new Vector2(hit0.transform.gameObject.transform.position.x - pos.x, hit0.transform.gameObject.transform.position.y - pos.y), Mathf.Infinity);
//				pos = new Vector2 (this.transform.position.x + 0.5f, this.transform.position.y - 0.5f);
//				RaycastHit2D hit3 = Physics2D.Raycast (pos , new Vector2(hit0.transform.gameObject.transform.position.x - pos.x, hit0.transform.gameObject.transform.position.y - pos.y), Mathf.Infinity);
//				pos = new Vector2 (this.transform.position.x + 0.5f, this.transform.position.y);
//				RaycastHit2D hit4 = Physics2D.Raycast (pos , new Vector2(hit0.transform.gameObject.transform.position.x - pos.x, hit0.transform.gameObject.transform.position.y - pos.y), Mathf.Infinity);
//				pos = new Vector2 (this.transform.position.x + 0.5f, this.transform.position.y + 0.5f);
//				RaycastHit2D hit5 = Physics2D.Raycast (pos , new Vector2(hit0.transform.gameObject.transform.position.x - pos.x, hit0.transform.gameObject.transform.position.y - pos.y), Mathf.Infinity);
//				pos = new Vector2 (this.transform.position.x, this.transform.position.y + 0.5f);
//				RaycastHit2D hit6 = Physics2D.Raycast (pos , new Vector2(hit0.transform.gameObject.transform.position.x - pos.x, hit0.transform.gameObject.transform.position.y - pos.y), Mathf.Infinity);
//				pos = new Vector2 (this.transform.position.x - 0.5f, this.transform.position.y + 0.5f);
//				RaycastHit2D hit7 = Physics2D.Raycast (pos , new Vector2(hit0.transform.gameObject.transform.position.x - pos.x, hit0.transform.gameObject.transform.position.y - pos.y), Mathf.Infinity);
//				pos = new Vector2 (this.transform.position.x - 0.5f, this.transform.position.y);
//				RaycastHit2D hit8 = Physics2D.Raycast (pos , new Vector2(hit0.transform.gameObject.transform.position.x - pos.x, hit0.transform.gameObject.transform.position.y - pos.y), Mathf.Infinity);
//				destinityPosition = hit0.transf	orm.gameObject.transform.position;
//				if (hit0.transform.gameObject.tag == "wall" && hit0.transform.gameObject == hit1.transform.gameObject && hit0.transform.gameObject == hit2.transform.gameObject 
//					&& hit0.transform.gameObject == hit3.transform.gameObject && hit0.transform.gameObject == hit4.transform.gameObject
//					&& hit0.transform.gameObject == hit5.transform.gameObject && hit0.transform.gameObject == hit6.transform.gameObject 
//					&& hit0.transform.gameObject == hit7.transform.gameObject && hit0.transform.gameObject == hit8.transform.gameObject) {
//					hooked = true;
//					destinyDirection = Vector3.Normalize (hit0.transform.position - this.transform.position);
//				}
//			}
//		}
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

//	void OnCollisionEnter2D(Collision2D col){
//		Debug.Log ("enter "+col.gameObject.name.ToString ());
//		if (col.gameObject.tag == "wall") {
//			rgbd.velocity = Vector2.zero;
//			hooked = false;
//		}
//	}
//
//	void OnCollisionStay2D(Collision2D col){
//		if (col.gameObject.tag == "wall" && col.gameObject.transform.position == destinityPosition) {
//			hooked = false;
//		}
//	}
//
//	void OnCollisionExit2D(Collision2D col){
//		if (!once && col.gameObject.tag == "wall")
//			once = false;
//	}
}
