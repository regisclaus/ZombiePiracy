using UnityEngine;
using System.Collections;

public class ZombieRespawnController : MonoBehaviour {

	public Transform[] zoombiesRespawns;

	private Transform respawnSelect;

	public Zombie zombieSelect;

	private float cooldownTimer;
	public float waitingTime = 2.0f;
	public float minWaintingTime = 0.5f;
	private float reduceTime = 0.0f;
	public float reduceWaintingTime = 10.0f;

	public int inicialSpeed = 3;
	public int maxSpeed = 9;
	public int countSpeed = 3;

	public bool noHasZombie3Lives = true;

	// Use this for init	ialization-0.2
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		canRespawn ();
		canReduceWaintingTime();
	}

	private void canReduceWaintingTime() {
		if (waitingTime > minWaintingTime) {
			reduceTime += Time.deltaTime;
			if (reduceTime > reduceWaintingTime) {
				waitingTime -= 0.1f;
				reduceTime = 0;
				if (countSpeed < maxSpeed) {
					countSpeed++;
					Debug.Log ("cs: " +countSpeed);
				}
				else {
					if(inicialSpeed < maxSpeed) {
						inicialSpeed++;
						Debug.Log ("is: " +inicialSpeed);
					}
				}
			}
		}
	}

	private void canRespawn () {
		cooldownTimer += Time.deltaTime;
		if(cooldownTimer > waitingTime){
			respawn();
			cooldownTimer = 0;
		}
	}
	
	public void respawn () {
		selectRespawn ();
		selectZombie ();


		Zombie zombieRespawned = Instantiate (zombieSelect, respawnSelect.position, respawnSelect.rotation) as Zombie;
		zombieSelect.actRespawnControllerDie = false;

		if (zombieRespawned.life == 1) {
			int speed = Random.Range (inicialSpeed, countSpeed);
			zombieRespawned.speedX = speed * 0.10f;
		} else {
			if (zombieRespawned.life == 2) {
				int speed = Random.Range (inicialSpeed, countSpeed);
				zombieRespawned.speedX = speed * 0.05f;
			}
		}
	}

	private void selectRespawn () {
		int indexSelected = Random.Range (0, zoombiesRespawns.Length);
		respawnSelect = zoombiesRespawns [indexSelected];
	}


	private void selectZombie() {
			int life = 1;


			if (inicialSpeed > 6 && noHasZombie3Lives) {
				life = Random.Range (1, 4);
			} else {
				if (countSpeed > 6) {
					life = Random.Range (1, 3);
				}
			}


			switch (life) {
			case 1: 
				zombieSelect.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animations/ZombieV1") as RuntimeAnimatorController;
				zombieSelect.life = 1;
				break;
			case 2:
				zombieSelect.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animations/ZombieV2") as RuntimeAnimatorController;
				zombieSelect.life = 2;
				break;
			case 3:
				zombieSelect.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animations/ZombieV3") as RuntimeAnimatorController;
				zombieSelect.life = 3;
				zombieSelect.actRespawnControllerDie = true;
				noHasZombie3Lives = false;
				break;
			}
	}
}
