using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour {

	public Transform[] zombiesRespawns;

	private int COOLDOWN_TIMER = 2;

	private float waitingTime = 0;

	public Zombie[] zombies;

	public Wave[] waves; // QuantityType

	void Awake () {
		//waitingTime = 0;
	}

	// Use this for initialization
	void Start () {
		Debug.Log (waves.Length);
		Debug.Log (waves[0].types.Length);
		StartCoroutine ("respawn");
	}
	
	// Update is called once per frame
	void Update () {

		//canRespawn();
	}

	private void canRespawn () {
		waitingTime += Time.deltaTime;
		if(waitingTime > COOLDOWN_TIMER){
			Debug.Log ("1");
			respawn();
			waitingTime = 0;
		}
	}


	IEnumerator respawn() {
	//private void respawn () {
		Debug.Log (waves.Length);	
		for(int i = 0; i < waves.Length; i++) {
			Debug.Log ("2");
			bool[] positionsSelected = {false, false, false, false}; 
			Wave wave = waves [i];

			for (int j = 0; j < wave.types.Length; j++) {
				Debug.Log ("3");

				int position = -1;
				do {
					position = Random.Range (0, 4);
				} while (positionsSelected[position] == true);
				positionsSelected [position] = true;

				Debug.Log ("4");

				//Transform respawnPositionSelected = selectRespawnPosition (positionsSelected);
				Instantiate (zombies[wave.types[j]], zombiesRespawns [position].position, Quaternion.identity);
			}
			yield return new WaitForSeconds(COOLDOWN_TIMER);

		}
		//yield return new WaitForSeconds(COOLDOWN_TIMER);
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

//[System.Serializable]
//public class ZombieType {
//	public int type;
//}
