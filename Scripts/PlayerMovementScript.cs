using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a bool assignmet outside the functions will automatically be false
// string is not Capatalized in Processing
//else if only runs if the previous if statement is false
//List<int> c = new List<int>(); a list can get infinely bigger, whereas an array cannot
//constructors:
//	void TestObject(){} //this is a constructor
//can pass through a trigger but not a collider
//getComponent is another way to get a script from a game Object or anything else
//GetComponent<RigidBody2D> ().AddForce(new Vector2(x,-y));
//Quarternion sets the rotation
//Destroy(this) Destroys the script not the gameobject
//NOT setting a variable to public automatically makes it private 

public class PlayerMovementScript : MonoBehaviour {

	public Rigidbody2D PlayRD;
	public SpriteRenderer PlaySR;
	public GameObject ArenaCamera;
	public AudioSource ArsenalSounds;

	public Sprite Stop; //when player isn't moving

	//all moving player variables
	public float speed = 3;
	public Sprite Walking1;
	public Sprite Walking2;
	public Sprite Walking3;
	public Sprite Walking4;
	public Sprite Walking5;
	public Sprite Walking6;
	public Sprite[] WalkingArr;
	public int walkNum = 0;
	public float walkDelay = 0.05f;
	public bool swingleft = false;
	public bool swingright = true;

	//all firing blaster variables
	public bool firing = false;
	public int handSwitch = 0;
	public float notFiring = 1;
	public Sprite HandLeft;
	public Sprite HandRight;
	public AudioClip blaster;

	//all firing laser variables
	public SpriteRenderer LaserSR;
	private float LaserTime = 1.24f;
	public bool firingL = false;
	public bool StanceOn = false;
	public bool StanceOff = true;
	public float LaserHold = 1;
	public float StanceDelay = 0.03f;
	public int stanceNum = 0;
	public Sprite laserOn;
	public Sprite Lstance1;
	public Sprite Lstance2;
	public Sprite Lstance3;
	public Sprite Lstance4;
	public Sprite[] LstanceArr;
	public int charge = 0;
	public AudioClip LaserShot;
	public BoxCollider2D LaserBox;
	//public GameObject LaserPrefab;

	// Use this for initialization
	void Start () {
		WalkingArr = new Sprite[]{Walking1,Walking2,Walking3,Walking4,Walking5,Walking6};
		LstanceArr = new Sprite[]{ Lstance1, Lstance2, Lstance3, Lstance4 };

		LaserBox.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		//WHEN THE PLAYER IS MOVING
		if (Input.GetKey (KeyCode.W)) {
			PlayRD.AddForce (new Vector3 (0, speed));
		}
		if (Input.GetKey (KeyCode.S)) {
			PlayRD.AddForce (new Vector3 (0, -speed));
		}
		if (Input.GetKey (KeyCode.A)) {
			PlayRD.AddForce (new Vector3 (-speed, 0));
		}
		if (Input.GetKey (KeyCode.D)) {
			PlayRD.AddForce (new Vector3 (speed, 0));
		}

		if ((Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)) && !firing && !firingL) {
			if (walkNum == 5) {
				swingleft = true;
				swingright = false;
			}
			if (walkNum == 1) {
				swingleft = false;
				swingright = true;
			}

			if (!swingleft && swingright) {
				if (walkDelay < 0) {
					++walkNum;
					walkDelay = 0.05f;
				}
			}
			if (swingleft && !swingright) {
				if (walkDelay < 0) {
					--walkNum;
					walkDelay = 0.05f;
				}
			}
			walkDelay -= Time.deltaTime;

			PlaySR.sprite = WalkingArr [walkNum];
		}
		else if(!firing && !firingL){
			PlaySR.sprite = Stop;
		}
		//=========================================================================================================================

		//WHEN THE PLAYER IS AIMING

		float camDis = Camera.main.transform.position.y - transform.position.y;

		Vector3 mouse = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, camDis));

		transform.rotation = Quaternion.LookRotation (Vector3.forward, mouse - transform.position);

		//==========================================================================================================================

		//WHEN PLAYER IS FIRING
		if(Input.GetMouseButtonDown(0) && !firingL){
			firing = true;
			notFiring = 1;
			handSwitch++;
			if (handSwitch % 2 == 0) {
				PlaySR.sprite = HandLeft;
			}
			else if (handSwitch % 2 != 0) {
				PlaySR.sprite = HandRight;
			}
			ArsenalSounds.PlayOneShot (blaster);
		}
		notFiring -= Time.deltaTime;
		if (notFiring < 0) {
			firing = false;
		}
		//==========================================================================================================================

		//WHEN PLAYER IS FIRING THE EYE LASER
		if(Input.GetMouseButtonDown(1) && charge >= 4){
			firingL = true;
			LaserTime = 0.2f;
			StanceOn = false;
			StanceOff = true;
			ArsenalSounds.PlayOneShot (LaserShot);
			LaserBox.enabled = true;

		}
		if (firingL) {
			LaserSR.sprite = laserOn;
					
			if(StanceDelay <= 0 && stanceNum != 3){
				++stanceNum;
				StanceDelay = 0.03f;
			}

			if (LaserTime <= 0) {
				stanceNum = 0;
				LaserSR.sprite = null;
				firingL = false;
				LaserBox.enabled = false;
				charge = 0;
			}

			PlaySR.sprite = LstanceArr[stanceNum];
			StanceDelay -= Time.deltaTime;
			LaserTime -= Time.deltaTime;

		}
	}

}
