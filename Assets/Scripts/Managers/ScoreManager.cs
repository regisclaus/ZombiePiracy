using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public long killScore = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void killZombie () {
		killScore++;
		Text killScoreText = GameObject.Find("ZombiesKilledScore").GetComponent<Text>();
		if (killScore / 100 > 0) {
			killScoreText.text = "0" + killScore;
		} else {
			if (killScore / 10 > 0) {
				killScoreText.text = "00" + killScore;
			}
			else {
				killScoreText.text = "000" + killScore;
			}
		}

	}
}
