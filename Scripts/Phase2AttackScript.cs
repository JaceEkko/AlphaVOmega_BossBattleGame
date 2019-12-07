using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2AttackScript : MonoBehaviour {

	public Phase1AttackScript P1AS;

	public float shotTime = 0.1f;

	public bool leftHand = true;

	public float shotDirect = 0;
	public float shotDirectX = 0;
	public float shotDirectY = -20;

	public float rotBoundLeft = -10.14f;
	public float rotBoundRight = 10.14f;
	public bool leftrot = true;
	public bool rightrot = false;

	public float turnTime = 3;
	public float turnNum = 0;

	public float switchtime;
	public int switchcount = 0;

	public float spinSpeed;

	public GameObject BOSS;

	public GameObject BulletPrefab;

	public AudioSource HandSounds;
	public AudioClip ShotSound;

	public BossDamageScript BDS;

	// Use this for initialization
	void Start () {
		switchtime = Random.Range(7, 25);
		spinSpeed = Random.Range(0.6f, 1.3f);
	}

	// Update is called once per frame
	void Update () {
		if (!P1AS.P1 && P1AS.P2 && !P1AS.P3) {
			P1AS.startSong2 = true;
			Vector3 NewPos = transform.position;
			NewPos.x = 0;
			NewPos.y = -24;
			transform.position = NewPos;

			shotTime -= Time.deltaTime;
			turnTime -= Time.deltaTime;
			switchtime -= Time.deltaTime;

			Vector3 HandRot = transform.rotation.eulerAngles;
			if (switchcount % 2 == 0) {
				HandRot.z += spinSpeed;
				if (HandRot.z < 90) {
					shotDirectX += spinSpeed;
					shotDirectY = -20;
				}
				if (HandRot.z > 90 && HandRot.z < 179) {
					shotDirectX -= spinSpeed;
					shotDirectY = 20;
				}
				if (HandRot.z > 180 && HandRot.z < 270) {
					shotDirectX -= spinSpeed;
					shotDirectY = 20;
				}
				if (HandRot.z > 270 && HandRot.z < 360) {
					shotDirectX += spinSpeed;
					shotDirectY = -20;
				}
			} else {
				HandRot.z -= spinSpeed;
				if (HandRot.z < 90) {
					shotDirectX -= spinSpeed;
					shotDirectY = -20;
				}
				if (HandRot.z > 90 && HandRot.z < 179) {
					shotDirectX += spinSpeed;
					shotDirectY = 20;
				}
				if (HandRot.z > 180 && HandRot.z < 270) {
					shotDirectX += spinSpeed;
					shotDirectY = 20;
				}
				if (HandRot.z > 270 && HandRot.z < 360) {
					shotDirectX -= spinSpeed;
					shotDirectY = -20;
				}
			}

			if (switchtime <= 0) {
				++switchcount;
				switchtime = Random.Range(7, 25);
				spinSpeed = Random.Range(0.6f, 1.3f);
			}

			transform.rotation = Quaternion.Euler (HandRot);


			if (shotTime <= 0 && !BDS.vulnerable) {
				GameObject player = Instantiate (BulletPrefab);
				Vector3 newGPos = player.transform.position;
				newGPos.x = transform.position.x;
				newGPos.y = transform.position.y;
				player.transform.position = newGPos;

				Rigidbody2D rd2 = player.GetComponent<Rigidbody2D> ();
				rd2.velocity = new Vector3 (shotDirectX, shotDirectY);
				shotTime = 0.1f;

				HandSounds.PlayOneShot (ShotSound);
			}

		}
	}



}
