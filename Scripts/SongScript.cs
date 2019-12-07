using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongScript : MonoBehaviour {

	public Phase1AttackScript P1AScript;

	public AudioSource P1AS;
	public AudioClip P1AC;
	public float restart1 = 7.98f;

	public AudioSource P2AS;
	public AudioClip P2AC;
	public bool startup2 = false;
	public float restart2 = 0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		restart1 -= Time.deltaTime;
		restart2 -= Time.deltaTime;
		if (P1AScript.startSong && restart1 <= 0) {
			P1AS.PlayOneShot (P1AC);

			if (P1AScript.startSong2 && restart1 <= 0) {
				P2AS.PlayOneShot (P2AC);
			}
		}

		if (restart1 <= 0) {
			restart1 = 7.98f;
		}
	}
}
