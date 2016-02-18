using System.Collections;
using System.Collections.Generic;
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

	public string[] dashUINames = {"Green Dash", "Blue Dash", "Yellow Dash", "Red Dash"};
	public Color[] outlineColors = {Color.black, new Color(6f/255f, 193f/255f, 255f/255f), new Color(234f/255f, 209f/255f, 0/255f), new Color(255f/255f, 6f/255f, 6f/255f)};
	public string[] playerTags = { "P1", "P2", "P3", "P4" };

	private CharacterSelectController _characterSelectController;
	private List<Vector3> playerPositions = new List<Vector3> ();
	private List<Vector3> playerRotations = new List<Vector3> ();
	private float scale = 1.2f;

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
		//_characterSelectController = GameObject.Find("Character Select Controller").GetComponent<CharacterSelectController>();

		playerPositions.Add (new Vector3 (-20.0f, 1.0f, 12.0f));
		playerPositions.Add (new Vector3 (20.0f, 1.0f, 12.0f));
		playerPositions.Add (new Vector3 (-20.0f, 1.0f, -12.0f));
		playerPositions.Add (new Vector3 (20.0f, 1.0f, -12.0f));

		playerRotations.Add (new Vector3 (0f, 125.0f, 0f));
		playerRotations.Add (new Vector3 (0f, 235.0f, 0f));
		playerRotations.Add (new Vector3 (0f, 45.0f, 0f));
		playerRotations.Add (new Vector3 (0f, 315.0f, 0f));


		GameObject ballGo = GameObject.FindGameObjectWithTag("Ball");
		_ballController = ballGo.GetComponent<BallController> ();
	}

	void Start(){
		//InstantiatePlayers ();
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

	public void InstantiatePlayers(){
		GameObject player;
		for (int i = 0; i < 4; i++) {
			print (playerRotations [i]);
			player = (GameObject) Instantiate (Resources.Load ("Prefabs/Characters/"+_characterSelectController.FinalSelections[i]), playerPositions[i], Quaternion.identity);
			player.transform.Rotate (playerRotations [i]);
			print (outlineColors [i]);
			player.GetComponent<Renderer> ().material.SetColor ("_OutlineColor", outlineColors [i]);
			player.name = _characterSelectController.FinalSelections [i];
			player.tag = "P"+(i+1);
			player.GetComponent<Player> ().dashUI = GameObject.Find (dashUINames [i]).GetComponent<Text>();
		}
	}
}
