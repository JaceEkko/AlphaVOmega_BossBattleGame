using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageScript : MonoBehaviour {

	public float healthReduce = 0.5f;
	public float healthShift = 2;

	public GameObject PlayerHealthBar;

	public SpriteRenderer PlaySR;

	public AudioSource HitAS;
	public AudioClip gotHit;

	public PlayerMovementScript PMS;

	//public Vector3 HealthBar;

	public GameObject ChargeMeter;
	public ChargeBarScript CBS;

	public Rigidbody2D PlayRD;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 ChargeBar = ChargeMeter.transform.localScale;
		if (PMS.charge == 0) {
			ChargeBar.x = 0;
		}
		ChargeMeter.transform.localScale = ChargeBar;
	}

	void OnTriggerEnter2D(Collider2D coll){
		Vector3 HealthBar = PlayerHealthBar.transform.localScale;
		Vector3 HealthBarS = PlayerHealthBar.transform.position;
		Vector3 ChargeBar = ChargeMeter.transform.localScale;

		if (coll.gameObject.name == "YellowBlast(Clone)") {
			HealthBar.x -= healthReduce;
			HealthBarS.x -= healthShift;

			PlaySR.color = new Color (255, 0, 0);

			HitAS.PlayOneShot (gotHit);

			PlayerHealthBar.transform.localScale = HealthBar;
			PlayerHealthBar.transform.position = HealthBarS;
		}

		if (coll.gameObject.name == "ChargeCore(Clone)") {
			HealthBar.x += healthReduce;
			HealthBarS.x += healthShift;
			ChargeBar.x += 2;

			PMS.charge++;

			if (PMS.charge < 4) {
				CBS.BarAS.PlayOneShot (CBS.coreCol);
			}
			if (PMS.charge == 4) {
				CBS.BarAS.PlayOneShot (CBS.chargeFull);
			}

			PlayerHealthBar.transform.localScale = HealthBar;
			PlayerHealthBar.transform.position = HealthBarS;
			ChargeMeter.transform.localScale = ChargeBar;
		}

		if (coll.gameObject.name == "BossLaser(Clone)") {
			HealthBar.x -= 3;
			HealthBarS.x -= 9;

			PlaySR.color = new Color (255, 0, 0);

			HitAS.PlayOneShot (gotHit);

			PlayRD.AddForce (new Vector3(0, PMS.speed + 2));

			PlayerHealthBar.transform.localScale = HealthBar;
			PlayerHealthBar.transform.position = HealthBarS;
		}
		Debug.Log("Getting Hit Here");

	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.name == "YellowBlast(Clone)"){
			PlaySR.color = new Color (255, 255, 255);
		}

		if (coll.gameObject.name == "ChargeCore(Clone)") {
			PlaySR.color = new Color (255, 255, 255);
		}

		if (coll.gameObject.name == "BossLaser(Clone)") {
			PlaySR.color = new Color (255, 255, 255);
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		Vector3 HealthBar = PlayerHealthBar.transform.localScale;
		Vector3 HealthBarS = PlayerHealthBar.transform.position;
		if (coll.gameObject.name == "PowerPod(Clone)") {
			HealthBar.x -= 3;
			HealthBarS.x -= 9;
			HitAS.PlayOneShot (gotHit);
		}
		PlayerHealthBar.transform.localScale = HealthBar;
		PlayerHealthBar.transform.position = HealthBarS;
	}
}
