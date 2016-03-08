using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Parse;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class RankingControler : MonoBehaviour {

	private Text rankingTextComponent;
	private string rankingText = "";

	// Use this for initialization
	void Start () {
		rankingTextComponent = GameObject.Find ("RankingText").GetComponent<Text> ();
		
		ParseObject.GetQuery("Carrier").OrderByDescending("recordZombiesKilled").FindAsync().ContinueWith(t => {
			IEnumerable results = t.Result;
			int indexRank = 1;
			foreach (ParseObject rank in results)
			{
				rankingText = rankingText + indexRank + "\t" + rank["userName"] + "\t\t\t" + rank["recordZombiesKilled"] + " \n";
				indexRank++;
				if(indexRank > 10) {
					break;
				}
			}
		});

	}
	
	// Update is called once per frame
	void Update () {
		rankingTextComponent.text = rankingText;
	}

	public void restartGame () {
		SceneManager.LoadScene("WelcomeSetupScene");
	}
	
}
