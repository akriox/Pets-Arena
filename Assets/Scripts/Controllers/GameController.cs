using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	private BallController _ballController;

	public float redScore { get; set; }
    public float greenScore { get; set; }
    public float blueScore { get; set; }
    public float yellowScore { get; set; }

    public Text messageUI;
    public Text redScoreUI;
	public Text greenScoreUI;
	public Text blueScoreUI;
	public Text yellowScoreUI;

	public Totem redTotem;
    public Totem greenTotem;
    public Totem blueTotem;
    public Totem yellowTotem;

	public static bool matchHasStarted = false;
	private bool matchIsOver;

	public static bool switchedZones = false;
	public static int scoringDirection = 1;
    public static float victoryScore = 15.0f;
    public static float scoringRate = 0.01f;

	public static int countdown = 3;

    void Awake() {
		matchIsOver = false;
		scoringDirection = 1;
		GameObject ballGo = GameObject.FindGameObjectWithTag("Ball");
		_ballController = ballGo.GetComponent<BallController> ();
	}

	void Start(){
		StartCoroutine (Countdown ());
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
                if (!switchedZones) IncreaseRedScore();
                else IncreaseGreenScore();
			    break;
				
		    case BallController.Zone.G:
                if (!switchedZones) IncreaseGreenScore();
                else IncreaseRedScore();
			    break;
				
		    case BallController.Zone.B:
                if (!switchedZones) IncreaseBlueScore();
                else IncreaseYellowScore();
			    break;
				
		    case BallController.Zone.Y:
                if (!switchedZones) IncreaseYellowScore();
                else IncreaseBlueScore();
			    break;
				
		    case BallController.Zone.N:
			    break;
		}
	}

	private IEnumerator Countdown(){
		for (int i = countdown; i >= 1; i--) {
			messageUI.text = i.ToString();
			messageUI.enabled = true;
			yield return new WaitForSeconds(1.0f);
		}
		StartCoroutine(DisplayMessage("Go !", 2.0f));
		matchHasStarted = true;
	}

    private void IncreaseYellowScore()
    {
        yellowScore += scoringDirection * scoringRate;
        if (yellowScore <= 0) yellowScore = 0;
        yellowScoreUI.text = yellowScore.ToString("0.00");
        yellowTotem.fill();
    }

    private void IncreaseRedScore()
    {
        redScore += scoringDirection * scoringRate;
        if (redScore <= 0) redScore = 0;
        redScoreUI.text = redScore.ToString("0.00");
        redTotem.fill();
    }

    private void IncreaseGreenScore()
    {
        greenScore += scoringDirection * scoringRate;
        if (greenScore <= 0) greenScore = 0;
        greenScoreUI.text = greenScore.ToString("0.00");
        greenTotem.fill();
    }

    private void IncreaseBlueScore()
    {
        blueScore += scoringDirection * scoringRate;
        if (blueScore <= 0) blueScore = 0;
        blueScoreUI.text = blueScore.ToString("0.00");
        blueTotem.fill();
    }

    private void displayWinner(string winner){
		messageUI.enabled = true;
		messageUI.text = winner + " wins !";
		matchIsOver = true;
	}

    private IEnumerator DisplayMessage(string msg, float duration)
    {
        messageUI.text = msg;
        messageUI.enabled = true;
        yield return new WaitForSeconds(duration);
        messageUI.text = "";
        messageUI.enabled = false;
    }
}
