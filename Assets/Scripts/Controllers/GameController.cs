using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	private BallController _ballController;

	public float redScore { get; set; }
    public float greenScore { get; set; }
    public float blueScore { get; set; }
    public float yellowScore { get; set; }

    public Text victoryUI;
    public Text redScoreUI;
	public Text greenScoreUI;
	public Text blueScoreUI;
	public Text yellowScoreUI;

	public TotemGauge redGauge;
    public TotemGauge greenGauge;
    public TotemGauge blueGauge;
    public TotemGauge yellowGauge;
	
	private bool matchIsOver;

	public static bool switchedZones;
	public static int scoringDirection = 1;
    public static float victoryScore = 15.0f;
    public static float scoringRate = 0.01f;

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

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.LoadLevel(0);
        if (Input.GetKeyDown(KeyCode.R))
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
		switch (_ballController.currentZone) {
		    case BallController.Zone.R:
			    redScore += scoringDirection * scoringRate;
			    if(redScore <= 0) redScore = 0;
			    redScoreUI.text = redScore.ToString ("0.00");
                redGauge.fill();
			    break;
				
		    case BallController.Zone.G: 
			    greenScore += scoringDirection * scoringRate;
			    if(greenScore <= 0) greenScore = 0;
			    greenScoreUI.text = greenScore.ToString ("0.00");
                greenGauge.fill();
			    break;
				
		    case BallController.Zone.B: 
			    blueScore += scoringDirection * scoringRate;
			    if(blueScore <= 0) blueScore = 0;
			    blueScoreUI.text = blueScore.ToString ("0.00");
                blueGauge.fill();
			    break;
				
		    case BallController.Zone.Y: 
			    yellowScore += scoringDirection * scoringRate;
			    if(yellowScore <= 0) yellowScore = 0;
			    yellowScoreUI.text = yellowScore.ToString ("0.00");
                yellowGauge.fill();
			    break;
				
		    case BallController.Zone.N:
			    break;
		}
	}

	private void displayWinner(string winner){
		victoryUI.enabled = true;
		victoryUI.text = winner + victoryUI.text;
		matchIsOver = true;
	}
}
