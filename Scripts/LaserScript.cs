using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour {

	public Rigidbody2D LaserRD;

	public float speed = 3;

	public float DeSpawn = 0.01f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		DeSpawn -= Time.deltaTime;

		if (Input.GetKey (KeyCode.W)) {
			LaserRD.AddForce (new Vector3 (0, speed));
		}
		if (Input.GetKey (KeyCode.S)) {
			LaserRD.AddForce (new Vector3 (0, -speed));
		}
		if (Input.GetKey (KeyCode.A)) {
			LaserRD.AddForce (new Vector3 (-speed, 0));
		}
		if (Input.GetKey (KeyCode.D)) {
			LaserRD.AddForce (new Vector3 (speed, 0));
		}

		if (DeSpawn <= 0) {
			Destroy (gameObject);
		}
	}
}
