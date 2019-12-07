using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour {

	public Camera InstructCam;
	public Camera cutout;
	public Camera MC;

	public float tillFight = 0.8f;
	public bool startTime = false;

	public GameObject Alpha; //0.93
	public GameObject Omega; //6.5, -0.98
	public GameObject VS; //3.1, -0.57
	public GameObject Rebooted; //9.71, -6.4

	public bool Vsdone = false;
	public bool AODone = false;
	public bool rebootDone = false;

	public AudioSource TitleAS;
	public AudioSource SongAS;
	public AudioClip boom;
	public AudioClip TitleSong;

	public float Songtime = 13.700f;

	public Camera MainCam;

	public Color Rgb;

	public SpriteRenderer PlaySR;
	public SpriteRenderer BossSR;
	public SpriteRenderer Hand1SR;
	public SpriteRenderer Hand2SR;
	public SpriteRenderer LaserPlay;
	public SpriteRenderer LaserBoss;
	public SpriteRenderer Thres;
	public SpriteRenderer PlayButSR;
	public SpriteRenderer InstructSR;

	public Sprite PlayShot;
	public Sprite BossShot;
	public Sprite Hand1;
	public Sprite Hand2;
	public Sprite LasPlay;
	public Sprite LasBoss;
	public Sprite Threslight;
	public Sprite PlayBut;
	public Sprite Instruct;

	public shakingScript AlphShake;
	public shakingScript OmeShake;
	public shakingScript VSShake;
	public shakingScript RebShake;

	public bool inInstruct = false;


	// Use this for initialization
	void Start () {
		Rgb = Color.gray;

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 alph = Alpha.transform.position;
		Vector3 ome = Omega.transform.position;
		Vector3 vs = VS.transform.position;
		Vector3 reb = Rebooted.transform.position;

		if(!Vsdone && vs.y >= -0.57){
			vs.y -= 0.3f;
		}
		if (!Vsdone && vs.y <= -0.57) {
			Vsdone = true;
			TitleAS.PlayOneShot (boom);
		}

		if(Vsdone && alph.x <= 0.93){
			alph.x += 0.3f;
		}
		if(Vsdone && ome.x >= 6.7){
			ome.x -= 0.35f;
		}

		if (!AODone && alph.x >= 0.93 && ome.x <= 6.7) {
			AODone = true;
			TitleAS.PlayOneShot (boom);
		}

		if (AODone && reb.y <= -6.8) {
			reb.y += 0.2f;
		}

		if (!rebootDone && reb.y >= -6.8) {
			rebootDone = true;
			MainCam.backgroundColor = Rgb;
			Songtime = 0;
			TitleAS.mute = true;

			PlaySR.sprite = PlayShot;
			BossSR.sprite = BossShot;
			Hand1SR.sprite = Hand1;
			Hand2SR.sprite = Hand2;
			LaserPlay.sprite = LasPlay;
			LaserBoss.sprite = LasBoss;
			Thres.sprite = Threslight;
			PlayButSR.sprite = PlayBut;
			InstructSR.sprite = Instruct;

			AlphShake.stat = true;
			OmeShake.stat = true;
			VSShake.stat = true;
			RebShake.stat = true;
		}

		if (rebootDone) {
			Songtime -= Time.deltaTime;
		}
		if (Songtime <= 0) {
			SongAS.PlayOneShot (TitleSong);
			Songtime = 13.700f;
		}

		Alpha.transform.position = alph;
		Omega.transform.position = ome;
		VS.transform.position = vs;
		Rebooted.transform.position = reb;

		if (Input.GetKey (KeyCode.I)) {
			InstructCam.enabled = true;
			MC.enabled = false;
			inInstruct = true;
		}
		if (!inInstruct) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				startTime = true;
				cutout.enabled = true;
				MC.enabled = false;

			}
		}

		if (startTime) {
			tillFight -= Time.deltaTime;
		}
		if (tillFight <= 0) {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("TheFightScene");
		}


	}
}
