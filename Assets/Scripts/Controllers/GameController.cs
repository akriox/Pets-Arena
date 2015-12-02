using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	private BallController _ballController;

	private float redScore;
	private float greenScore;
	private float blueScore;
	private float yellowScore;

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

	void Awake() {
		matchIsOver = false;
		GameObject ballGo = GameObject.FindGameObjectWithTag("Ball");
		_ballController = ballGo.GetComponent<BallController> ();
	}
	
	void FixedUpdate () {
		updateScore ();
		victoryCheck ();

		if(matchIsOver && Input.GetKeyDown(KeyCode.R))
			Application.LoadLevel(Application.loadedLevel);
	}

	void victoryCheck(){
		if (!matchIsOver) {
			if(yellowScore > victoryScore){
				victoryUI.enabled = true;
				victoryUI.text = "Yellow"+victoryUI.text;
				matchIsOver = true;
			}
			else if(redScore > victoryScore){
				victoryUI.enabled = true;
				victoryUI.text = "Red"+victoryUI.text;
				matchIsOver = true;
			}
			else if(greenScore > victoryScore){
				victoryUI.enabled = true;
				victoryUI.text = "Green"+victoryUI.text;
				matchIsOver = true;
			}
			else if(blueScore > victoryScore){
				victoryUI.enabled = true;
				victoryUI.text = "Blue"+victoryUI.text;
				matchIsOver = true;
			}
		}
	}

	void updateScore (){
		switch(_ballController.currentZone){

			case BallController.Zone.R:
				redScore += 0.01f;
				redScoreUI.text = redScore.ToString("0.00");
				updateCylindersColors(1,0,0,0);
				break;

			case BallController.Zone.G: 
				greenScore += 0.01f;
				greenScoreUI.text = greenScore.ToString("0.00");
				updateCylindersColors(0,1,0,0);
				break;

			case BallController.Zone.B: 
				blueScore += 0.01f;
				blueScoreUI.text = blueScore.ToString("0.00");
				updateCylindersColors(0,0,1,0);
				break;

			case BallController.Zone.Y: 
				yellowScore += 0.01f;
				yellowScoreUI.text = yellowScore.ToString("0.00");
				updateCylindersColors(0,0,0,1);
				break;

			case BallController.Zone.N:
				updateCylindersColors(0,0,0,0);
				break;
		}
	}

	void updateCylindersColors(int r, int g, int b, int y){
		redCylinder.GetComponent<Renderer> ().material.color = r == 1 ? Color.red : Color.black;
		greenCylinder.GetComponent<Renderer> ().material.color = g == 1 ? Color.green : Color.black;
		blueCylinder.GetComponent<Renderer> ().material.color = b == 1 ? Color.blue : Color.black;
		yellowCylinder.GetComponent<Renderer>().material.color = y == 1 ? Color.yellow : Color.black;
	}
}
