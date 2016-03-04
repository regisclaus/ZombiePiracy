using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Zombie : MonoBehaviour {

	public float speedX = 0.3f;
	public float speedY = 0.5f;
	
	private Vector2 directionVector = new Vector2(-1, 0);
	
	private Vector2 movementVector;

	public int life = 1;

	private CoinManager coinManager;

	private ScoreManager scoreManager;

	private GameManager gameManager;
	
	public bool actRespawnControllerDie  = false;


	// Use this for initialization
	void Start () {

		GameObject coinManagerGM = GameObject.Find("CoinManager");
		if (coinManagerGM != null) {
			coinManager = coinManagerGM.GetComponent<CoinManager> ();
		}

		GameObject scoreManagerGM = GameObject.Find("ScoreManager");
		if (scoreManagerGM != null) {
			scoreManager = scoreManagerGM.GetComponent<ScoreManager> ();
		}


		GameObject gameManagerGm = GameObject.Find("GameManager");
		if (gameManagerGm != null) {
			gameManager = gameManagerGm.GetComponent<GameManager> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		move ();
	}

	void FixedUpdate() {
		applyMovement ();
	}

	public void takeDamage(int damage) {
		life -= damage;
		if (life <= 0) {
			executeZombie ();
		} else {
			animateTakingDamage();
		}
	}

	void executeZombie () {
		respawnCoin ();
		getScore ();

		if (actRespawnControllerDie) {
			GameObject.Find("ZombieRespawnController").GetComponent<ZombieRespawnController>().noHasZombie3Lives = true;
		}
		Destroy (this.gameObject);
	}

	void respawnCoin() {
		coinManager.respawnCoin(this.transform.position);
	}

	void getScore () {
			scoreManager.killZombie();
	}
	
	// Move o tiro. Cria um vetor de movimento para set aplicado depois em FixedUpdate
	void move() {
		movementVector = new Vector2(speedX * directionVector.x, speedY * directionVector.y);
	}
	
	// Aplica o movimento ao rigidbody
	void applyMovement() {
		this.GetComponent<Rigidbody2D>().velocity = this.movementVector;
	}

	// Verifica Colisao 2D,
	void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.tag == "Cannon")
		{
			gameManager.gameOver();
		}
	}

	void animateTakingDamage() {
		GetComponent<Animator> ().SetTrigger ("takingDamage");
//		(Resources.Load ("Animations/ZombieTakingDamage") as AnimationClip).
	}
}
