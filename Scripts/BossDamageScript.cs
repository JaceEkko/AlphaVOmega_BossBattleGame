using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageScript : MonoBehaviour {

	public NextPhaseScript NPS;
	public Phase1AttackScript P1AS;

	public SpriteRenderer BossSR;
	public Sprite Open1;
	public Sprite Open2;
	public Sprite Open3;
	public Sprite Open4;
	public Sprite[] OpenArr;
	public Sprite RebootDone;
	public int OpenNum = 0;
	public float OpenDelay = 0.05f;

	public bool vulnerable = false;
	public float VulDelay = 3;

	public GameObject BossHealthBar;

	public float rebootTime = 6;
	public bool rebooted = true;

	public AudioSource BossAS;
	public AudioClip Hit;
	public AudioClip SrtCircuit;


	// Use this for initialization
	void Start () {
		OpenArr = new Sprite[]{ Open1, Open2, Open3, Open4 };
	}
	
	// Update is called once per frame
	void Update () {
		if (vulnerable) {
			OpenDelay -= Time.deltaTime;
			rebootTime -= Time.deltaTime;
			BossSR.sprite = OpenArr[OpenNum];
			if (P1AS.P1 && !P1AS.P2 && !P1AS.P3) {
				if (OpenNum < 3 && OpenDelay <= 0 && rebooted) {
					++OpenNum;
					OpenDelay = 0.05f;
				}
			}
		}

		if (rebootTime <= 0) {
			rebooted = false;
			OpenDelay -= Time.deltaTime;
			BossSR.color = new Color (255, 255, 255);
			BossSR.sprite = OpenArr[OpenNum];
			if (OpenNum > -1 && OpenDelay <= 0 && !rebooted) {
				--OpenNum;
				OpenDelay = 0.05f;
			}
			if (OpenNum == 0) {
				rebooted = true;
				if (P1AS.P1 && !P1AS.P2 && !P1AS.P3) {
					BossSR.sprite = RebootDone;
				}
				if (!NPS.PH1 && NPS.PH2 && !NPS.PH3) {
					BossSR.sprite = NPS.P2Damage;
				}
				rebootTime = 6;
				vulnerable = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.name == "Laser") {
			vulnerable = true;
			BossAS.PlayOneShot (SrtCircuit);
		}

		Vector3 BossHealth = BossHealthBar.transform.localScale;
		if (P1AS.P1 && vulnerable && coll.gameObject.name == "RedBlast(Clone)") {
			BossSR.color = new Color (255, 0, 0);
			BossHealth.x -= 0.2f;
			BossAS.PlayOneShot (Hit);
		}
		if (P1AS.P2 && coll.gameObject.name == "RedBlast(Clone)") {
			BossSR.color = new Color (255, 0, 0);
			BossHealth.x -= 0.1f;
			BossAS.PlayOneShot (Hit);
		}
		if (P1AS.P2 && coll.gameObject.name == "Laser") {
			BossSR.color = new Color (255, 0, 0);
			BossHealth.x -= 0.5f;
			BossAS.PlayOneShot (Hit);
		}
		if (P1AS.P3 && coll.gameObject.name == "RedBlast(Clone)") {
			BossSR.color = new Color (255, 0, 0);
			BossHealth.x -= 0.2f;
			BossAS.PlayOneShot (Hit);
		}
		BossHealthBar.transform.localScale = BossHealth;
	}

	void OnTriggerExit2D(Collider2D coll){
		if (vulnerable && coll.gameObject.name == "RedBlast(Clone)") {
			BossSR.color = new Color (255, 255, 255);
		}
		if (P1AS.P2 && coll.gameObject.name == "RedBlast(Clone)") {
			BossSR.color = new Color (255, 255, 255);
		}
		if (P1AS.P2 && coll.gameObject.name == "Laser") {
			BossSR.color = new Color (255, 255, 255);
		}
		if (P1AS.P3 && coll.gameObject.name == "RedBlast(Clone)") {
			BossSR.color = new Color (255, 255, 255);
		}
	}
}
