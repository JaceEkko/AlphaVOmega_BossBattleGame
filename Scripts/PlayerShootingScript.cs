using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingScript : MonoBehaviour {

	public GameObject CameraObj;

	public GameObject BulletPrefab;

	public int handSwitch = 0;

	public PlayerMovementScript PMS;

	public float aimAngle = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			++handSwitch;
			if (handSwitch % 2 == 0) {
				GameObject player = Instantiate (BulletPrefab);
				Vector3 newGPos = player.transform.position;
				newGPos.x = transform.position.x - 4;
				newGPos.y = transform.position.y + 4;
				player.transform.position = newGPos;

				float camDis = Camera.main.transform.position.y - transform.position.y;

				Vector3 mouse = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, camDis));

				Rigidbody2D rd2 = player.GetComponent<Rigidbody2D> ();
				rd2.velocity = new Vector3 (mouse.x, mouse.y);

			}
			else if (handSwitch % 2 != 0) {
				GameObject player = Instantiate (BulletPrefab);
				Vector3 newGPos = player.transform.position;
				newGPos.x = transform.position.x + 4;
				newGPos.y = transform.position.y + 4;
				player.transform.position = newGPos;

				Vector3 mouse = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y/*, camDis*/));

				Rigidbody2D rd2 = player.GetComponent<Rigidbody2D> ();
				rd2.velocity = new Vector3 (mouse.x, mouse.y);


			}
		}


	}
}
