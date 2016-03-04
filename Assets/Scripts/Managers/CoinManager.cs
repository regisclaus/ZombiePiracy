using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Parse;

public class CoinManager : MonoBehaviour {

	public GameObject coin;

	public int coinsCollected = 0;

	// Canvas onde a moeda vai aparecer
	[SerializeField]
	private GameObject canvas;

	// Use this for initialization
	void Start () {
		canvas = GameObject.FindWithTag ("Canvas");

		var query = ParseObject.GetQuery("Carrier");
		query.WhereEqualTo("userObjectId", ParseUser.CurrentUser.ObjectId).OrderByDescending("updateAt").FirstAsync().ContinueWith(t =>
		                                                                                      {
			ParseObject results = (ParseObject) t.Result;
			coinsCollected = results.Get<int>("coins");
		});

	}
	
	// Update is called once per frame
	void Update () {
		setCoinText ();
	}

	public void respawnCoin(Vector3 zombiePosition) {
		int percent = Random.Range (0, 100);
		if (percent > 40) {
			GameObject c = Instantiate (coin, zombiePosition, Quaternion.identity) as GameObject;
			c.transform.parent = canvas.transform;
		}
	}

	public void getCoin () {
		coinsCollected++;
		setCoinText ();

	}

	public void setCoinText () {
		Text coinsScoreText = GameObject.Find("CoinsScore").GetComponent<Text>();
		if (coinsCollected / 100 > 0) {
			coinsScoreText.text = "0" + coinsCollected;
		} else {
			if (coinsCollected / 10 > 0) {
				coinsScoreText.text = "00" + coinsCollected;
			}
			else {
				coinsScoreText.text = "000" + coinsCollected;
			}
		}
	}
}
