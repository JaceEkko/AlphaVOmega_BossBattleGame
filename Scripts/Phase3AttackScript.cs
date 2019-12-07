using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase3AttackScript : MonoBehaviour {

	public NextPhaseScript NPS;

	public float attackY; //will be random
	public float attackX; //will be set to specific values on the screen (left: -388.9f right: -117.2f)
	public float XsideDetermine = 0;
	public float BackForth = 0;
	public bool dontSwitch = false;
	public bool leftSide = false;
	public bool rightSide = false;

	public float laserTime = 2;
	public bool shotLaser = false;

	public GameObject LaserPrefab;

	public bool justShot = false;

	public float speed = 0.8f;
	public float speedChangeNum = 0;
	public float speedChange1 = 1.3f;
	public float speedChange2 = 2.0f;

	public AudioSource BossAS;
	public AudioClip LasShot;


	// Use this for initialization
	void Start () {
		attackY = Random.Range (-44f, 44f);
	}

	// Update is called once per frame
	void Update () {

		if (!NPS.PH1 && !NPS.PH2 && NPS.PH3) {
			Vector3 attackCoord = transform.position;
			Vector3 attackAng = transform.rotation.eulerAngles;

			if (XsideDetermine % 2 == 0 && !dontSwitch) {
				attackCoord.x = 137;
				attackAng.z = 270;
				dontSwitch = true;
				leftSide = true;
			} 
			if(XsideDetermine % 2 != 0 && !dontSwitch) {
				attackCoord.x = -137;
				attackAng.z = 90;
				dontSwitch = true;
				rightSide = true;
			}

			if (dontSwitch && rightSide && attackCoord.x <= -105 && !justShot) {
				Debug.Log ("AC1: " + attackCoord.x);
				attackCoord.x += speed;
			}
			if (dontSwitch && leftSide && attackCoord.x >= 105 && !justShot) {
				Debug.Log ("AC1: " + attackCoord.x);
				attackCoord.x -= speed;
			}

			if (dontSwitch && rightSide && attackCoord.x >= -105 && !justShot) {
				if (!shotLaser) {
					GameObject pod = Instantiate (LaserPrefab);
					Vector3 newGPos = pod.transform.position;
					newGPos.x = transform.position.x + 180;
					newGPos.y = transform.position.y;
					pod.transform.position = newGPos;
					BossAS.PlayOneShot (LasShot);
					shotLaser = true;
				}
				laserTime -= Time.deltaTime;
			}
			if (dontSwitch && leftSide && attackCoord.x <= 105 && !justShot) {
				if (!shotLaser) {
					GameObject pod = Instantiate (LaserPrefab);
					Vector3 newGPos = pod.transform.position;
					newGPos.x = transform.position.x - 180;
					newGPos.y = transform.position.y;
					pod.transform.position = newGPos;
					BossAS.PlayOneShot (LasShot);
					shotLaser = true;
				}
				laserTime -= Time.deltaTime;
			}

			if (laserTime <= 0) {
				justShot = true;
				++BackForth;
			}

			if (dontSwitch && rightSide && attackCoord.x >= -139 && justShot) { //-137
				Debug.Log ("AC2: " + attackCoord.x);
				attackCoord.x -= 0.8f;
			}
			if (dontSwitch && leftSide && attackCoord.x <= 139 && justShot) { //137
				Debug.Log ("AC2: " + attackCoord.x);
				attackCoord.x += 0.8f;
			}

			if (dontSwitch && rightSide && attackCoord.x <= -139 && BackForth % 2 != 0) { //-137
				++XsideDetermine;
				++BackForth;
				++speedChangeNum;
				dontSwitch = false;
				attackY = Random.Range (-44f, 44f);
				attackCoord.y = attackY;
				justShot = false;
				shotLaser = false;
				laserTime = 2;
				rightSide = false;
			}
			if (dontSwitch && leftSide && attackCoord.x >= 139 && justShot && BackForth % 2 != 0) { //137
				++XsideDetermine;
				++BackForth;
				++speedChangeNum;
				dontSwitch = false;
				attackY = Random.Range (-44f, 44f);
				attackCoord.y = attackY;
				justShot = false;
				shotLaser = false;
				laserTime = 2;
				leftSide = false;

			}

			if (speedChangeNum >= 5 && speedChangeNum <= 10) {
				speed = speedChange1;
			}
			if (speedChangeNum >= 5 && speedChangeNum <= 10) {
				speed = speedChange2;
			}

			transform.position = attackCoord;
			transform.rotation = Quaternion.Euler (attackAng);
		}
		
	}
}
