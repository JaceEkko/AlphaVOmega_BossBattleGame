using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	public float DespawnTime = 2.3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		DespawnTime -= Time.deltaTime;
		if (DespawnTime <= 0) {
			Destroy (gameObject);
		}
	}
}
