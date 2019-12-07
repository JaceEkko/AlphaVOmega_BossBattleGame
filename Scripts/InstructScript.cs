using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructScript : MonoBehaviour {

	public GameObject CameraInstruct;
	public Camera CI;
	public Camera MC;

	public TitleScript TS;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (TS.inInstruct) {
			Vector3 MoveCam = CameraInstruct.transform.position;
			if (Input.GetKey (KeyCode.LeftArrow) && MoveCam.x >= 61.1f) {
				MoveCam.x -= 0.3f;
			}
			if (Input.GetKey (KeyCode.RightArrow) && MoveCam.x <= 72.8f) {
				MoveCam.x += 0.3f;
			}
			CameraInstruct.transform.position = MoveCam;
			if (Input.GetKey (KeyCode.B)) {
				CI.enabled = false;
				MC.enabled = true;
				TS.inInstruct = false;
			}

		}
		
	}
}
