using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	private BallController _ballController;

	public float redScore;
	public float greenScore;
	public float blueScore;
	public float yellowScore;

	public Text redScoreUI;
	public Text greenScoreUI;
	public Text blueScoreUI;
	public Text yellowScoreUI;

	public GameObject redCylinder;
	public GameObject greenCylinder;
	public GameObject blueCylinder;
	public GameObject yellowCylinder;

	public float victoryScore;
	public Text victoryUI;
	private bool matchIsOver;
	public bool switchedZones;

	public int scoringDirection;

	void Awake() {
		matchIsOver = false;
		scoringDirection = 1;
		GameObject ballGo = GameObject.FindGameObjectWithTag("Ball");
		_ballController = ballGo.GetComponent<BallController> ();
	}

	void Update(){
		if (!matchIsOver){
			updateScore ();
			victoryCheck ();
		}

		if(Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
		if(Input.GetKeyDown(KeyCode.R))
			Application.LoadLevel(Application.loadedLevel);
	}

	private void victoryCheck(){
		if(yellowScore > victoryScore){
			displayWinner("Yellow");
		}
		else if(redScore > victoryScore){
			displayWinner("Red");
		}
		else if(greenScore > victoryScore){
			displayWinner("Green");
		}
		else if(blueScore > victoryScore){
			displayWinner("Blue");
		}
	}

	private void updateScore (){
		if (!switchedZones) {
			switch (_ballController.currentZone) {
			case BallController.Zone.R:
				redScore += scoringDirection * 0.01f;
				if(redScore <= 0)
					redScore = 0;
				redScoreUI.text = redScore.ToString ("0.00");
				updateCylindersColors (1, 0, 0, 0);
				break;
				
			case BallController.Zone.G: 
				greenScore += scoringDirection * 0.01f;
				if(greenScore <= 0)
					greenScore = 0;
				greenScoreUI.text = greenScore.ToString ("0.00");
				updateCylindersColors (0, 1, 0, 0);
				break;
				
			case BallController.Zone.B: 
				blueScore += scoringDirection * 0.01f;
				if(blueScore <= 0)
					blueScore = 0;
				blueScoreUI.text = blueScore.ToString ("0.00");
				updateCylindersColors (0, 0, 1, 0);
				break;
				
			case BallController.Zone.Y: 
				yellowScore += scoringDirection * 0.01f;
				if(yellowScore <= 0)
					yellowScore = 0;
				yellowScoreUI.text = yellowScore.ToString ("0.00");
				updateCylindersColors (0, 0, 0, 1);
				break;
				
			case BallController.Zone.N:
				updateCylindersColors (0, 0, 0, 0);
				break;
			}
		} else {
			switch (_ballController.currentZone) {
			case BallController.Zone.R:
				greenScore += scoringDirection * 0.01f;
				if(greenScore <= 0)
					greenScore = 0;
				greenScoreUI.text = greenScore.ToString ("0.00");
				updateCylindersColors (0, 1, 0, 0);
				break;
				
			case BallController.Zone.G: 
				redScore += scoringDirection * 0.01f;
				if(redScore <= 0)
					redScore = 0;
				redScoreUI.text = redScore.ToString ("0.00");
				updateCylindersColors (1, 0, 0, 0);
				break;
				
			case BallController.Zone.B: 
				yellowScore += scoringDirection * 0.01f;
				if(yellowScore <= 0)
					yellowScore = 0;
				yellowScoreUI.text = yellowScore.ToString ("0.00");
				updateCylindersColors (0, 0, 0, 1);
				break;

			case BallController.Zone.Y: 
				blueScore += scoringDirection * 0.01f;
				if(blueScore <= 0)
					blueScore = 0;
				blueScoreUI.text = blueScore.ToString ("0.00");
				updateCylindersColors (0, 0, 1, 0);
				break;
				
			case BallController.Zone.N:
				updateCylindersColors (0, 0, 0, 0);
				break;
			}
		}
	}

	private void updateCylindersColors(int r, int g, int b, int y){
		redCylinder.GetComponent<Renderer> ().material.color = r == 1 ? Color.red : Color.black;
		greenCylinder.GetComponent<Renderer> ().material.color = g == 1 ? Color.green : Color.black;
		blueCylinder.GetComponent<Renderer> ().material.color = b == 1 ? Color.blue : Color.black;
		yellowCylinder.GetComponent<Renderer>().material.color = y == 1 ? Color.yellow : Color.black;
	}

	private void displayWinner(string winner){
		victoryUI.enabled = true;
		victoryUI.text = winner + victoryUI.text;
		matchIsOver = true;
	}
}
