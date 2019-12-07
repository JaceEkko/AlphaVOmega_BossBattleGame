using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPhaseScript : MonoBehaviour {

	public Camera P1CAM;
	public Camera P2CAM;
	public GameObject P3CAM;

	public Phase1AttackScript P1AS;
	public Phase1AttackScript P1AS2;

	public SpriteRenderer BossSR;
	public SpriteRenderer Handright;
	public SpriteRenderer HandLeft;
	public Sprite P2Damage;
	public Sprite explode;

	public BossDamageScript BDS;
	public PlayerDamageScript PDS;

	public GameObject RightHand;
	public GameObject LeftHand;
	public GameObject Boss;

	public PolygonCollider2D LeftHandBox;

	public bool PH1 = true;
	public bool PH2;
	public bool PH3;

	public bool inNxtPhase1 = false;
	public bool inNxtPhase2 = false;

	public float explodeTime = 0.4f;
	public bool dontCount1 = false;
	public bool dontCount2 = false;

	public AudioSource HRAS;
	public AudioSource HLAS;
	public AudioClip boom;

	public SongScript SS;

	public float deathTime = 1.5f;

	public TextMesh TMWhoWon;
	public float TimeTillWhoWon = 2;
	public bool countDown = false;
	public bool youdied = false;

	public AudioSource Boss3AS;
	public AudioClip deathScream;
	public AudioClip deathScream2;

	// Use this for initialization
	void Start () {
		Camera.main.enabled = true;
		P1CAM.enabled = false;
		P2CAM.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (BDS.BossHealthBar.transform.localScale.x < 30 /*&& BDS.BossHealthBar.transform.localScale.x > 30*/ && !inNxtPhase1) { //change back to 30
			
			P1AS.P1 = false;
			if (!dontCount1) {
				Handright.sprite = explode;
				BDS.vulnerable = false;
				BossSR.sprite = P2Damage;
				LeftHandBox.isTrigger = false;
				BossSR.color = new Color (255, 255, 255);
				explodeTime -= Time.deltaTime;
			}
			if (explodeTime <= 0 && !dontCount1) {
				HRAS.PlayOneShot(boom);
				Destroy (RightHand);
				explodeTime = 0.4f;
				dontCount1 = true;
			}

			if (dontCount1) {
				Vector3 moveHand = LeftHand.transform.position;
				if (moveHand.x <= 0) {
					moveHand.x += 0.6f;
				}
				if (moveHand.y >= -24) {
					moveHand.y -= 0.6f;
				}
				LeftHand.transform.position = moveHand;

				if (moveHand.x >= 0 && moveHand.y <= -24) {
					P1AS.P2 = true;
					PH1 = P1AS.P1;
					inNxtPhase1 = true;

				}

			}

		}
		if (BDS.BossHealthBar.transform.localScale.x < 15 && !inNxtPhase2) { //change back to 25
			P1AS.P2 = false;
			P1AS.P1 = false;
			PH2 = false;
			PH1 = false;
			if (!dontCount2) {
				HandLeft.sprite = explode;
				BDS.vulnerable = false;
				BossSR.sprite = P2Damage;
				BossSR.color = new Color (255, 255, 255);
				explodeTime -= Time.deltaTime;
			}
			if (explodeTime <= 0 && !dontCount2) {
				HLAS.PlayOneShot(boom);
				Destroy (LeftHand);
				dontCount2 = true;
				explodeTime = 0.4f;
			}

			if (dontCount2) {
				Vector3 moveBoss = Boss.transform.position;
				if (moveBoss.y <= 98.6f) {
					moveBoss.y += 0.6f;
				}
				Boss.transform.position = moveBoss;

				if (moveBoss.y >= 98.6f) {
					P1AS.P3 = true;
					PH3 = true;
					PH1 = P1AS.P1;
					inNxtPhase2 = true;

				}

			}

		}
		if (BDS.BossHealthBar.transform.localScale.x <= 0) {
			deathTime -= Time.deltaTime;

			SS.P1AS.mute = true;
			SS.P2AS.mute = true;
			PH3 = false;
			PH2 = false;
			PH1 = false;
			Vector3 shake = Boss.transform.position;
			shake.x = Random.Range (Boss.transform.position.x - 0.1f, Boss.transform.position.x + 0.1f);
			shake.y = Random.Range (Boss.transform.position.y - 0.1f, Boss.transform.position.y + 0.1f);
			transform.position = shake;

			if (deathTime <= 0) {
				Vector3 explodeVec = Boss.transform.localScale;
				explodeVec.x = 10; 
				explodeVec.y = 10;
				Boss.transform.localScale = explodeVec;
				BossSR.sprite = explode;
				explodeTime -= Time.deltaTime;

			}

			if (explodeTime <= 0) {
				BossSR.sprite = null;

				Boss3AS.PlayOneShot (deathScream);
				countDown = true;
				explodeTime = 100;
			}

			if (countDown) {
				TimeTillWhoWon -= Time.deltaTime;
			}

			if (TimeTillWhoWon <= 0) {
				P1CAM.enabled = true;
				Camera.main.enabled = false;
			}
		}

		if (PDS.PlayerHealthBar.transform.localScale.x <= 0) {
			youdied = true;
			P2CAM.enabled = true;
			SS.P1AS.mute = true;
			SS.P2AS.mute = true;
			Camera.main.enabled = false;
			if (youdied) {
				Boss3AS.PlayOneShot (deathScream2);
				youdied = false;
			}
		}
	}
}
