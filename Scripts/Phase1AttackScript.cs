using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1AttackScript : MonoBehaviour {

	public bool P1 = true;
	public bool P2 = false;
	public bool P3 = false;

	public bool startSong = false;
	public bool startSong2 = false;

	public float shotTime = 0.3f;

	public bool leftHand = true;
	//public bool rightHand = false;

	public float shotDirect = 0;

	public float rotBoundLeft = -10.14f;
	public float rotBoundRight = 10.14f;
	public bool leftrot = true;
	public bool rightrot = false;

	public float turnTime = 3;
	public float turnNum = 0;
	//public float timeInc = 0;

	public GameObject BOSS;

	public GameObject BulletPrefab;

	public AudioSource HandSounds;
	public AudioClip ShotSound;

	public BossDamageScript BDS;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (P1 && !P2 && !P3) {
			startSong = true;
			shotTime -= Time.deltaTime;
			turnTime -= Time.deltaTime;

			Vector3 HandRot = transform.rotation.eulerAngles;
			if (leftHand) {
				if (leftrot && !rightrot) {
					HandRot.z += 0.8f;
					shotDirect += 0.8f;
				}

				if (rightrot && !leftrot) {
					HandRot.z -= 0.8f;
					shotDirect -= 0.8f;
				}
			}
			if (!leftHand) {
				if (leftrot && !rightrot) {
					HandRot.z -= 0.8f;
					shotDirect -= 0.8f;
				}

				if (rightrot && !leftrot) {
					HandRot.z += 0.8f;
					shotDirect += 0.8f;
				}
			}

			if (turnTime <= 0 && turnNum % 2 == 0) {
				++turnNum;
				leftrot = false;
				rightrot = true;
				turnTime = 3;
			} 
			if (turnTime <= 0 && turnNum % 2 != 0) {
				++turnNum;
				leftrot = true;
				rightrot = false;
				turnTime = 3;
			}


			transform.rotation = Quaternion.Euler (HandRot);


			if (shotTime <= 0 && leftHand && !BDS.vulnerable) {
				GameObject player = Instantiate (BulletPrefab);
				Vector3 newGPos = player.transform.position;
				newGPos.x = transform.position.x - 1;
				newGPos.y = transform.position.y - 6;
				player.transform.position = newGPos;

				Rigidbody2D rd2 = player.GetComponent<Rigidbody2D> ();
				rd2.velocity = new Vector3 (shotDirect, -20);
				shotTime = 0.3f;

				HandSounds.PlayOneShot (ShotSound);
			}

			if (shotTime <= 0 && !leftHand && !BDS.vulnerable) {
				GameObject player = Instantiate (BulletPrefab);
				Vector3 newGPos = player.transform.position;
				newGPos.x = transform.position.x + 1;
				newGPos.y = transform.position.y - 6;
				player.transform.position = newGPos;

				Rigidbody2D rd2 = player.GetComponent<Rigidbody2D> ();
				rd2.velocity = new Vector3 (shotDirect, -20);
				shotTime = 0.3f;

				HandSounds.PlayOneShot (ShotSound);
			}
		}


	}
}
