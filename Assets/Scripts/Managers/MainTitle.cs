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
		SceneManager.LoadScene ("HistoryScene");
	}
	
}
