using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreScript : MonoBehaviour {

	public AudioSource CoreAS;
	public AudioClip Radiating;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CoreAS.PlayOneShot(Radiating);
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.name == "The Player") {
			Destroy (gameObject);
		}
	}
}
