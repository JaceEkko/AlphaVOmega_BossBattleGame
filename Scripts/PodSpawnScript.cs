using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodSpawnScript : MonoBehaviour {

	public float PodSpawn = 10;
	public GameObject PodPrefab;

	public BossDamageScript BDS;
	public Phase1AttackScript P1AS;

	public GameObject Target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		PodSpawn -= Time.deltaTime;
		if (PodSpawn <= 0 && !BDS.vulnerable && P1AS.P1) {
			GameObject player = Instantiate (PodPrefab);
			PodScript ps = player.GetComponent<PodScript> ();
			ps.PMS = Target; //setting ps.PMS to the gameObject Target tells the pos which object to follow, which is anything with a PlayerMovementScript
			Vector3 newGPos = player.transform.position;
			newGPos.x = transform.position.x;
			newGPos.y = transform.position.y - 7;
			player.transform.position = newGPos;
			PodSpawn = 10;
		}
		if (PodSpawn <= 0 && !BDS.vulnerable && P1AS.P2) {
			GameObject player = Instantiate (PodPrefab);
			PodScript ps = player.GetComponent<PodScript> ();
			ps.PMS = Target;
			Vector3 newGPos = player.transform.position;
			newGPos.x = transform.position.x;
			newGPos.y = transform.position.y - 7;
			player.transform.position = newGPos;
			PodSpawn = 10;
		}

	}
}
