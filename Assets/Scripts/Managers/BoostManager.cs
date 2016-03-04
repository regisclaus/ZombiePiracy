using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoostManager : MonoBehaviour {

	enum BoostStatus {Ready, Using, Cooldown, NotReady};

	public bool drillShootActivate = false;

	public bool doubleShootActivate = false;

	public bool powerShootActivate = false;

	public CoinManager coinManager;

	private float drillInUseTime;
	private float drillInCooldownTime;
	private float DRILL_COOLDOWN_TIME = 10.0f;
	private float DRILL_DURATION_TIME = 10.0f;
	public Button drillButton;
	private int DRILL_COST = 10;
	public Text drillCostTxt;

	private BoostStatus drillStatus = BoostStatus.Ready;

	// Use this for initialization
	void Start () {
	
//		if (PlayerPrefs.GetInt ("drillShootBoost") == 1) {
//			drillShootActivate = true;
//		}
//
//		if (PlayerPrefs.GetInt ("doubleShootBoost") == 1) {
//			doubleShootActivate = true;
//		}
//
//		if (PlayerPrefs.GetInt ("powerShootBoost") == 1) {
//			powerShootActivate = true;
//		}
	}
	
	// Update is called once per frame
	void Update () {
		drillControling ();
	}

	private void drillControling () {


		if (drillStatus == BoostStatus.Ready && isDrillHasMoney () == true) {
			drillStatus = BoostStatus.Ready;
			drillInReady ();
		}

		if (drillStatus == BoostStatus.Ready && isDrillHasMoney () == false) {
			drillStatus = BoostStatus.NotReady;
			drillInNotReady ();
		}

		if (drillStatus == BoostStatus.NotReady && isDrillHasMoney () == true) {
			drillStatus = BoostStatus.Ready;
			drillInReady ();
		}

		if (drillStatus == BoostStatus.NotReady && isDrillHasMoney () == false) {
			drillStatus = BoostStatus.NotReady;
			drillInNotReady ();
		}

		if (drillStatus == BoostStatus.Using && isDrillUseFineshed () == false) {
			drillStatus = BoostStatus.Using;
			drillInUse ();
		}

		if (drillStatus == BoostStatus.Using && isDrillUseFineshed () == true) {
			drillStatus = BoostStatus.Cooldown;
			drillStartCooldown ();
		}

		if (drillStatus == BoostStatus.Cooldown && isDrillCooldownFinished () == false) {
			drillStatus = BoostStatus.Cooldown;
			drillInCooldown ();
		}

		if (drillStatus == BoostStatus.Cooldown && isDrillCooldownFinished () == true && isDrillHasMoney () == true) {
			drillStatus = BoostStatus.Ready;
		}

		if (drillStatus == BoostStatus.Cooldown && isDrillCooldownFinished () == true && isDrillHasMoney () == false) {
			drillStatus = BoostStatus.NotReady;
		}

	}

	private void drillInNotReady () {

		drillCostTxt.GetComponent<RectTransform> ().localScale = new Vector3(1,1,1);
		drillCostTxt.color = Color.red;
		drillCostTxt.text = DRILL_COST + " coins";

		drillButton.enabled = false;
	}

	private void drillInReady () {

		drillCostTxt.GetComponent<RectTransform> ().localScale = new Vector3(1,1,1);
		drillCostTxt.color = Color.green;
		drillCostTxt.text = DRILL_COST + " coins";

		drillButton.enabled = true;
	}


	private bool isDrillHasMoney () {
		if (coinManager.coinsCollected >= DRILL_COST) {
			return true;
		}
		return false;
	}


	public void activateDrillShot () {
		drillStartUse ();
	}

	private void drillStartUse () {
		drillStatus = BoostStatus.Using;
		drillBuy ();
		drillShootActivate = true;
		drillInUseTime = 0;

		drillButton.enabled = false;
	}

	private bool isDrillUseFineshed () {
		if (drillInUseTime > DRILL_DURATION_TIME) {
			return true;
		}
		return false;
	}


	private void drillInUse () {
		drillInUseTime += Time.deltaTime;

		drillCostTxt.GetComponent<RectTransform> ().localScale = new Vector3(2,2,1);
		drillCostTxt.color = Color.cyan;
		drillCostTxt.text = ((int)drillInUseTime).ToString ();
	}

	private void drillBuy () {
		coinManager.coinsCollected -= DRILL_COST;
	}

	private void drillInCooldown () {
		drillInCooldownTime += Time.deltaTime;

		drillCostTxt.GetComponent<RectTransform> ().localScale = new Vector3(2,2,1);
		drillCostTxt.color = Color.gray;
		drillCostTxt.text = ((int)drillInCooldownTime).ToString ();
	}


	private void drillStartCooldown () {
		drillInCooldownTime = 0;
		drillShootActivate = false;
	}

	private bool isDrillCooldownFinished () {
		if (drillInCooldownTime > DRILL_COOLDOWN_TIME) {
			return true;
		}
		return false;
	}
		
//	public void activateDoubleShot () {
//		drillShootActivate = true;
//	}
//
//	public void activatePowerShot () {
//		powerShootActivate = true;
//	}


}
