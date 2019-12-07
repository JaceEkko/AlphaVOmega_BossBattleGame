using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakingScript : MonoBehaviour {

	//Vector3 PosOrg = transform.position;
	public float posx;
	public float posy;

	public bool stat = true;

	// Use this for initialization
	void Start () {
		posx = transform.position.x;
		posy = transform.position.y;
	}

	// Update is called once per frame
	void Update () {
		Vector3 shake = transform.position;
		if (!stat) {
			posx = shake.x;
			posy = shake.y;
		}
		if (stat) {
			
			shake.x = Random.Range (posx - 0.05f, posx + 0.05f);
			shake.y = Random.Range (posy - 0.05f, posy + 0.05f);

		}
		transform.position = shake;
	}
}
