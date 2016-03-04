using UnityEngine;
using System.Collections;

public class Pirate : MonoBehaviour {

	// Use this for initialization
	void Start () { 

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void animateShotting () {
		GetComponent<Animator>().SetTrigger ("Shoot");
	}

}
