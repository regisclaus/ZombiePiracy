using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

	public Transform bulletRespawn;

	public Bullet bullet;

	private float cooldownTimer;
	public float waitingTime = 0.3f;
	private bool isCanShoot = true;

	private Animator animator;

	public Pirate pirate;
	public GameObject crate;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		cooldowning ();
	}

	//void OnMouseUp () {
	public void ShootButton () {
		//if (verifyDoubleShootActivated ()) {
		//	shoot ();
		//}
		shoot ();
	}

	public void shoot () {
		if (isCanShoot == true && isGamePaused() == false) {
			Instantiate (bullet, bulletRespawn.position, bulletRespawn.rotation);
			//if (verifyDoubleShootActivated ()) {
			//	Instantiate (bullet, bulletRespawn.position, bulletRespawn.rotation);
			//}
			isCanShoot = false;
			shootAnimation();
		}
	}

	private bool isGamePaused () {
		GameObject gameManagerGM = GameObject.Find("GameManager");
		if (gameManagerGM != null) {
			GameManager gameManager = gameManagerGM.GetComponent<GameManager> ();
			return gameManager.paused;
		}
		return false;
	}

	private void cooldowning () {
		if (isCanShoot == false) {
			cooldownTimer += Time.deltaTime;
			if (cooldownTimer > waitingTime) {
				isCanShoot = true;
				cooldownTimer = 0;
			}
		}
	}

	private void shootAnimation() {
		animator.SetTrigger("Shoot");
		//pirate.animateShotting ();
		//crate.GetComponent<Animator> ().SetTrigger ("Shoot");
	}


	private bool verifyDoubleShootActivated () {
		BoostManager boostManager = GameObject.Find("BoostManager").GetComponent<BoostManager>();
		if (boostManager.doubleShootActivate) {
			return true;
		}
		return false;
	}
		
}
