using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour {

	public GameObject stage1Modal;

	public GameObject stage1Moda2;

	public GameObject stage1Moda3;

	public GameObject stage1Moda4;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void openStage1Modal () {
		stage1Modal.SetActive (true);
	}

	public void closeStage1Modal () {
		stage1Modal.SetActive (false);
	}

	public void loadShipScene () {
		SceneManager.LoadScene ("ShipScene");
	}

	public void loadStage1Scenario() {
		SceneManager.LoadScene ("TropicalScenarioScene");
	}
}
