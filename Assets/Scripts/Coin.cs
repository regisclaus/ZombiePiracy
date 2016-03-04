using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	private float cooldownTimer;
	public float waitingTime = 10.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		canHideCoin ();
	}
		
	// void OnMouseUp () {
	public void MouseGrab() {
		touchCoin ();
	}

	private void touchCoin () {
		if (isGamePaused () == false) {
			GameObject coinManagerGM = GameObject.Find ("CoinManager");
			if (coinManagerGM != null) {
				CoinManager coinManager = coinManagerGM.GetComponent<CoinManager> ();
				coinManager.getCoin ();
				Destroy (this.gameObject);
			}
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

	private void canHideCoin () {
		cooldownTimer += Time.deltaTime;
		if(cooldownTimer > waitingTime){
			hideCoin();
			cooldownTimer = 0;
		}
	}

	private void hideCoin () {
		Destroy (this.gameObject);
	}
}
