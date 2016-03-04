using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class History : MonoBehaviour {

	public Image comic;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void showNextComic () {
		if (comic != null) {
			comic.GetComponent<Animator> ().SetTrigger ("start");
		} else {
			skipHistory ();
		}
	}

	public void skipHistory () {
		SceneManager.LoadScene ("TropicalStageScene");
	}
}
