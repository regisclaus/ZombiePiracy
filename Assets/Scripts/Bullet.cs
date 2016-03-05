using UnityEngine;
using System.Collections;

public class Bullet: MonoBehaviour {

	private float speedX = 1.0f;
	
	private Vector2 directionVector = new Vector2(1, 0);
	
	private Vector2 movementVector;

	public int damage = 1;

	private bool firstVisibleFalse = false;

	// Use this for initialization
	void Start () {
//		if (verifyPowerShootActivated ()) {
//			setPowerShoot();
//		}
	}
	
	// Update is called once per frame
	void Update () {

//		this.GetComponent<Rigidbody>.AddForce(new Vector3(0,2000,0));
		move ();
		verifyCollision ();
	}
	
	void FixedUpdate() {
		applyMovement ();
	}
	
	// Move o tiro. Cria um vetor de movimento para set aplicado depois em FixedUpdate
	void move() {
		movementVector = new Vector2(speedX * directionVector.x, directionVector.y);
	}
	
	// Aplica o movimento ao rigidbody
	void applyMovement() {
		this.GetComponent<Rigidbody2D>().velocity = this.movementVector;
	}

	// Verifica se o tiro colide com algo
	void verifyCollision() {
		verifyCollisionOutSideCamera ();
	}
	
	// Verifica se o tiro ja não esta sendo visto pela camera, se não estiver sendo visto o gameObject é destruido
	void verifyCollisionOutSideCamera() {
		if (!this.GetComponent<Renderer>().isVisible && firstVisibleFalse == true) {
			Destroy(this.gameObject);
		}
		if (!this.GetComponent<Renderer>().isVisible && firstVisibleFalse == false) {
			firstVisibleFalse = true;
		}
	}

	// Verifica Colisao 2D,
	void OnCollisionEnter2D(Collision2D coll){
		verifyCollisionWithZombie (coll);
	}

	private void verifyCollisionWithZombie (Collision2D coll) {
		if(coll.gameObject.tag == "Zombie")
		{
			coll.gameObject.SendMessage("takeDamage", this.damage);
//			if(verifyDrillShootActivated() == false) {
				Destroy(this.gameObject);
//			}
//			else {
//				Physics2D.IgnoreCollision(coll.collider, GetComponent<Collider2D>());
//			}
		}
	}

	private bool verifyDrillShootActivated () {
		BoostManager boostManager = GameObject.Find("BoostManager").GetComponent<BoostManager>();
		if (boostManager.drillShootActivate) {
			return true;
		}
		return false;
	}

	private bool verifyPowerShootActivated () {
		BoostManager boostManager = GameObject.Find("BoostManager").GetComponent<BoostManager>();
		if (boostManager.powerShootActivate) {
			return true;
		}
		return false;
	}

	private void setPowerShoot() {
		setAnimationPowerShoot ();
		setDoubleDamage ();
	}

	private void setAnimationPowerShoot() {
		GetComponent<Animator>().SetTrigger("powerShoot");
	}

	private void setDoubleDamage() {
		damage = 2;
	}
}
