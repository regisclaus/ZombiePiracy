using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NicknameManager : MonoBehaviour {

	public InputField nicknameInput;

	public Text invalidNickname;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void play () {
		string nickname = nicknameInput.text;
		if (nickname.Length < 4 || nickname.Length > 16) {
			invalidNickname.gameObject.SetActive (true);
		} else {
			PlayerPrefs.SetString ("nickname", nickname);
			SceneManager.LoadScene ("HistoryScene");
		}
	}

	public void clearErrorMsg () {
		invalidNickname.gameObject.SetActive (false);
	}
}
