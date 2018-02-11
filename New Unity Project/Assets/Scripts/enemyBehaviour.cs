using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour {

	public float velocity = 0.7f;
	public List<Vector2> positions;
	public bool walking = false;
	public bool forward = true;
	public int pos = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (walking) {
			if (Vector2.Distance(this.transform.position, positions[pos]) < 0.5f) {
				if (pos == positions.Count - 1) forward = false;// end of loop
				else if (pos == 0) forward = true;
				pos += (forward) ?  1 : -1;
			}
			this.transform.position = Vector2.Lerp(positions[pos], this.transform.position, velocity);
		}
	}
}
