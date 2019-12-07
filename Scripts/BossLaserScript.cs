using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserScript : MonoBehaviour {
	public float shotTime = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		shotTime -= Time.deltaTime;

		if (shotTime <= 0) {
			Destroy (gameObject);
		}
	}
}
