using UnityEngine;
using System.Collections;
using Parse;

public class GameManager : MonoBehaviour {

	public bool paused = false;
	public bool gameOvered = false;

	public GameObject gameOverModal;
	public GameObject pausedModal;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void gameOver () {
		pauseTime ();
		gameOverModal.SetActive (true);
		gameOvered = true;
		saveCoinsCollected();
	}

	private void saveCoinsCollected () {
		CoinManager coinManager = GameObject.Find("CoinManager").GetComponent<CoinManager>();
		ScoreManager scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

		var query = ParseObject.GetQuery("Carrier");

		query.WhereEqualTo("userObjectId", ParseUser.CurrentUser.ObjectId).FirstAsync().ContinueWith(t =>
		                                                                                     {
			ParseObject carrier = (ParseObject) t.Result;

			Debug.Log (carrier);
			carrier["coins"] = (int) coinManager.coinsCollected;
			carrier["maxZombiesKilled"] = (long) carrier["maxZombiesKilled"] + scoreManager.killScore;
			carrier["recordZombiesKilled"] = Mathf.Max(
				((long)carrier["recordZombiesKilled"]), scoreManager.killScore);
			carrier["playTimes"] = ((long) carrier["playTimes"]) + 1;

			carrier.SaveAsync();
			Debug.Log ("Updated");

		});


	}

	public void restartGame () {
		unPauseGame ();
		Application.LoadLevel("TropicalStageScene");
	}

	public void rankingGame () {
		unPauseGame ();
		Application.LoadLevel("RankingScene");
	}

	public void exitGame() {
		unPauseGame ();
		Application.LoadLevel("WelcomeSetupScene");
	}

	public void pauseGame() {
		pauseTime ();
		pausedModal.SetActive (true);
	}

	public void pauseTime () {
		Time.timeScale = 0;
		paused = true;
	}

	public void unPauseGame() {
		Time.timeScale = 1;
		paused = false;
		pausedModal.SetActive (false);
	}
}
