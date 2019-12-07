using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodScript : MonoBehaviour {

	public Rigidbody2D PodRD;

	public GameObject PMS;
	public GameObject CorePrefab;

	public SpriteRenderer PodSR;

	public Sprite Explode;

	public int PodHitCount = 1;

	public AudioSource PodAS;
	public AudioSource PodAS2;
	public AudioClip Hit;
	public AudioClip Boom;

	public float timeTillBoom = 0.05f;

	public bool HitPlayer = false;

	public PlayerDamageScript PDS;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 PlayMove = PMS.transform.position;
		Vector3 PodMove = transform.position;
		Vector3 chase = PodRD.velocity;
		if (PodMove.x < PlayMove.x) {
			chase.x += 0.5f;
		}
		else if (PodMove.x > PlayMove.x) {
			chase.x -= 0.5f;
		}
		if (PodMove.y < PlayMove.y) {
			chase.y += 0.5f;
		}
		else if (PodMove.y > PlayMove.y) {
			chase.y -= 0.5f;
		}
		PodRD.velocity = chase;
		transform.position = PodMove;
		PMS.transform.position = PlayMove;

		if (PodHitCount <= 0 || HitPlayer) {
			timeTillBoom -= Time.deltaTime;
			PodAS2.PlayOneShot (Boom);
			PodSR.sprite = Explode;
			if (timeTillBoom <= 0) {
				GameObject pod = Instantiate (CorePrefab);
				Vector3 newGPos = pod.transform.position;
				newGPos.x = transform.position.x;
				newGPos.y = transform.position.y;
				pod.transform.position = newGPos;
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.name == "RedBlast(Clone)") {
			PodSR.color = new Color (255, 0, 0);
			PodHitCount--;
			PodAS2.PlayOneShot (Hit);
		}


	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.name == "RedBlast(Clone)") {
			PodSR.color = new Color (255, 255, 255);
		}
	}

//	void OnCollisionEnter2D(Collision2D coll){
//		Vector3 HealthBar = PDS.PlayerHealthBar.transform.localScale;
//		Vector3 HealthBarS = PDS.PlayerHealthBar.transform.position;
//		if (coll.gameObject.name == "The Player") {
//			HealthBar.x -= 3;
//			HealthBarS.x -= 3;
//			HitPlayer = true;
//			PDS.HitAS.PlayOneShot (Hit);
//		}
//		PDS.PlayerHealthBar.transform.localScale = HealthBar;
//		PDS.PlayerHealthBar.transform.position = HealthBarS;
//	}
}
