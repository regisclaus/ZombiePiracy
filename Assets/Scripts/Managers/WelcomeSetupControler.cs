using UnityEngine;
using System.Collections;
using Parse;
using UnityEngine.UI;
using System.Threading.Tasks;

public class WelcomeSetupControler : MonoBehaviour {

	private string zombiesKilled;
	private string record;
	private string username;
	private string coins;
	private int position = 1;
	private bool positionFound = false;
	public GameObject usernameModal;
	public Text invalidUsernameMessageTxt;
	private ParseUser user;
	private bool hasInvalidUsernameMessage = true;
	private string invalidUsernameMessage;
	

	// Use this for initialization
	void Start () {
		setupLogin ();
	}
	
	// Update is called once per frame
	void Update () {
		showTxts ();
		showErrors ();
		verifyCloseUserModal ();
	}

	void setupLogin() {
		user = ParseUser.CurrentUser;
		if (user == null) {
			usernameModal.SetActive (true);
		} else {
			usernameModal.SetActive (false);
			setupTxts ();
		}

	}

	void setupTxts () {

		var query = ParseObject.GetQuery("Carrier");
		query.WhereEqualTo ("userObjectId", user.ObjectId).OrderByDescending ("updateAt").FirstAsync ().ContinueWith (t => {
			ParseObject results = (ParseObject)t.Result;
			zombiesKilled = results.Get<int> ("maxZombiesKilled").ToString ();
			record = results.Get<int> ("recordZombiesKilled").ToString ();
			username = user.Username;
			coins = results.Get<int>("coins").ToString();
		});
	}

	void findRankAndShowTxts () {
		if (username != null && positionFound == false) {
			positionFound = true;
			ParseObject.GetQuery ("Carrier").OrderByDescending ("recordZombiesKilled").FindAsync ().ContinueWith (t1 => {
				IEnumerable resultsRank = t1.Result;
				foreach (ParseObject carrier in resultsRank) {
					if (username.Equals (carrier.Get<string> ("userName"))) {

						break;
					}
					position++;
				}
			});
		}
	}

	void showTxts () {
		GameObject.Find("TXT_Username").GetComponent<Text>().text = username;
		GameObject.Find("TXT_ZombiesKilled").GetComponent<Text>().text = zombiesKilled;
		GameObject.Find("TXT_Record").GetComponent<Text>().text = record;
		GameObject.Find("TXT_Coins").GetComponent<Text>().text = coins;
		if (positionFound) {
			GameObject.Find ("TXT_Position").GetComponent<Text> ().text = position.ToString ();
		}
		findRankAndShowTxts ();
		showBoostsOnOff ();
	}

	void showErrors () {
		if (hasInvalidUsernameMessage == true) {
			invalidUsernameMessageTxt.gameObject.SetActive (true);
			invalidUsernameMessageTxt.text = invalidUsernameMessage;
		} else {
			invalidUsernameMessageTxt.gameObject.SetActive (false);
		}
	}

	void verifyCloseUserModal () {
		if (usernameModal.activeSelf && hasInvalidUsernameMessage == false) {
			closeUsernameModal();
		}
	}

	public void logout () {
		ParseUser.LogOutAsync();
		usernameModal.SetActive (true);
		resetFindRank ();

	}

	private void resetFindRank () {
		username = null;
		positionFound = false;
		position = 1;
	}

	public void tryCreateLogin () {
		string usernameSelected = GameObject.Find ("INP_Username").GetComponent<InputField> ().text;

		if (usernameSelected.Equals ("")) {
			hasInvalidUsernameMessage = true;
			invalidUsernameMessage = "username is empty";
		} else {
			createLogin ();
		}
	}

	private void createLogin () {
		user = new ParseUser()
		{
			Username = GameObject.Find("INP_Username").GetComponent<InputField>().text,
			Password = "1234"
		};
		Task task = user.SignUpAsync();
		task.ContinueWith (t => {
			if (t.IsFaulted) {
				foreach (ParseException parseException in task.Exception.InnerExceptions) {

					hasInvalidUsernameMessage = true;
					invalidUsernameMessage = parseException.Message;
					Debug.Log("ParseManager.signUpNewUser fault code: " + parseException.Code + ", Message " + parseException.Message);
				}
			}
			else if (t.IsCanceled) {
				hasInvalidUsernameMessage = true;
				print("ParseManager.signUpNewUser cancel ");
			}
			else {
				ParseObject carrier = new ParseObject("Carrier");
				carrier["userObjectId"] = user.ObjectId;
				carrier["userName"] = user.Username;
				carrier["coins"] = 0;
				carrier["maxZombiesKilled"] = 0;
				carrier["recordZombiesKilled"] = 0;
				carrier["firstBoostUnlocked"] = false;
				carrier["secondBoostUnlocked"] = false;
				carrier["thirdBoostUnlocked"] = false;
				carrier["playTimes"] = 0;
				carrier.SaveAsync().ContinueWith (t1 => { hasInvalidUsernameMessage = false;});
			}
		});
	}

	private void closeUsernameModal () {
		GameObject.Find("INP_Username").GetComponent<InputField>().text = "";
		usernameModal.SetActive (false);
		setupTxts ();
	}

	public void play () {
		Application.LoadLevel("MainGame");
	}

	public void rankingGame () {
		Application.LoadLevel("RankingScene");
	}
	

	// DEPRECTED


	private void showBoostsOnOff (){
		if (PlayerPrefs.GetInt ("drillShootBoost") == 1) {
			GameObject.Find ("TXT_Boost1").GetComponent<Text> ().text = "Drill Shoot\nOn";
		} else {
			GameObject.Find ("TXT_Boost1").GetComponent<Text>().text = "Drill Shoot\nOff";
		}

		if (PlayerPrefs.GetInt ("doubleShootBoost") == 1) {
			GameObject.Find ("TXT_Boost2").GetComponent<Text>().text = "Double Shoot\nOn";
		} else {
			GameObject.Find ("TXT_Boost2").GetComponent<Text> ().text = "Double Shoot\nOff";
		}

		if (PlayerPrefs.GetInt ("powerShootBoost") == 1) {
			GameObject.Find ("TXT_Boost3").GetComponent<Text>().text = "Power Shoot\nOn";
		}
		else {
			GameObject.Find ("TXT_Boost3").GetComponent<Text>().text = "Power Shoot\nOff";
		}
	}

	public void selectFirstBoost () {
		if (PlayerPrefs.GetInt ("drillShootBoost") == 1) {
			PlayerPrefs.SetInt ("drillShootBoost", 0);
		}
        else {
			PlayerPrefs.SetInt ("drillShootBoost", 1);
		}
	}

	public void selectSecondBoost () {
		if (PlayerPrefs.GetInt ("doubleShootBoost") == 1) {
			PlayerPrefs.SetInt ("doubleShootBoost", 0);
		}
		else {
			PlayerPrefs.SetInt ("doubleShootBoost", 1);

		}
	}

	public void selectThirdBoost () {
		if (PlayerPrefs.GetInt ("powerShootBoost") == 1) {
			PlayerPrefs.SetInt ("powerShootBoost", 0);
		}
		else {
			PlayerPrefs.SetInt ("powerShootBoost", 1);
		}
	}


}
