using UnityEngine;
using System.Collections;
using Parse;
using UnityEngine.SceneManagement;

public class MainTitle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void play () {
		if (PlayerPrefs.GetString ("nickname") == null || PlayerPrefs.GetString ("nickname").Equals ("")) {
			Debug.Log (PlayerPrefs.GetString ("nickname"));
			SceneManager.LoadScene ("NicknameScene");
		} else {
			if (PlayerPrefs.GetInt ("tutorialDone") == 1) {
				SceneManager.LoadScene ("ShipScene");
			} else {
				SceneManager.LoadScene ("HistoryScene");
			}
		}
	}
	
}
