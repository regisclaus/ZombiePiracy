using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour {

	public Transform[] zombiesRespawns;

	private int COOLDOWN_TIMER = 2;

	public Zombie[] zombies;

	public Wave[] waves;

	public GameManager gameManager;

	private bool allZombiesRespawned = false;

	// Use this for initialization
	void Start () {
		StartCoroutine ("respawn");
	}
	
	// Update is called once per frame
	void Update () {
		verifyAllZombiesKilled ();
	}

	IEnumerator respawn() {
		for(int i = 0; i < waves.Length; i++) {
			bool[] positionsSelected = {false, false, false, false}; 
			Wave wave = waves [i];
			for (int j = 0; j < wave.types.Length; j++) {
				int position = -1;
				do {
					position = Random.Range (0, 4);
				} while (positionsSelected[position] == true);
				positionsSelected [position] = true;
				Instantiate (zombies[wave.types[j]], zombiesRespawns [position].position, Quaternion.identity);
			}
			yield return new WaitForSeconds(COOLDOWN_TIMER);
		}
		allZombiesRespawned = true;
	}

	private void verifyAllZombiesKilled () {
		if (allZombiesRespawned) {
			if(GameObject.FindObjectsOfType<Zombie> ().Length == 0) {
				gameManager.victoryGame ();
				Destroy (this.gameObject);
			}
		}
	}

	private Transform selectRespawnPosition (bool[] positionsSelected) {
		int position = -1;
		do {
			position = Random.Range (0, 4);
		} while (positionsSelected[position] != true);
		positionsSelected [position] = true;
		return zombiesRespawns [position];
	}	

}

[System.Serializable]
public class Wave {
	public int[] types;
}