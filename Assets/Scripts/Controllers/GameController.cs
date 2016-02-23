using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	private BallController _ballController;
	private HeadUpDisplay HUD;
	private Sprite[] countDownSprites;

	public float redScore { get; set; }
    public float greenScore { get; set; }
    public float blueScore { get; set; }
    public float yellowScore { get; set; }

	public Totem redTotem;
    public Totem greenTotem;
    public Totem blueTotem;
    public Totem yellowTotem;

	private string[] powerUpHudNames = {"GreenPower", "BluePower", "YellowPower", "RedPower"};
	private string[] sideDashHudNames = {"GreenSideDash", "BlueSideDash", "YellowSideDash", "RedSideDash"};
	private Color[] outlineColors = {new Color(58f/255f, 1f, 32f/255f), new Color(6f/255f, 193f/255f, 1f), new Color(234f/255f, 209f/255f, 0f), new Color(1f, 6f/255f, 6f/255f)};
	private Material[] trailMat;

	private CharacterSelectController _characterSelectController;
	private List<Vector3> playerPositions = new List<Vector3> ();
	private List<Vector3> playerRotations = new List<Vector3> ();

	public static bool matchHasStarted = false;
	private bool matchIsOver;

	public static bool switchedZones = false;
	public static int scoringDirection = 1;
    public static float victoryScore = 15.0f;
    public static float scoringRate = 0.01f;

	public static int countdown = 4;

    void Awake() {
		HUD = new HeadUpDisplay();
		HUD.Init();
		countDownSprites = Resources.LoadAll<Sprite>("2D/HUD/CountDown");

		trailMat = Resources.LoadAll<Material>("Materials/Characters/Trails");

		matchIsOver = false;
		scoringDirection = 1;

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

		_characterSelectController = GameObject.Find("Character Select Controller").GetComponent<CharacterSelectController>();
		InstantiatePlayers ();
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
			SceneManager.LoadScene("MainMenu");
		/*
        if (Input.GetKeyDown(KeyCode.R))
			SceneManager.LoadScene("LD_Forest");
		*/
	}

	private void victoryCheck(){
		if(greenScore >= victoryScore){
			HUD.DisplaySprite(HUD.winnerSprite[0]);
			matchIsOver = true;
		}
		else if(blueScore >= victoryScore){
			HUD.DisplaySprite(HUD.winnerSprite[1]);
			matchIsOver = true;
		}
		else if(yellowScore >= victoryScore){
			HUD.DisplaySprite(HUD.winnerSprite[2]);
			matchIsOver = true;
		}
		else if(redScore >= victoryScore){
			HUD.DisplaySprite(HUD.winnerSprite[3]);
			matchIsOver = true;
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
		for (int i = 0; i < countdown; i++) {
			StartCoroutine( HUD.DisplaySprite(countDownSprites[i], 1.0f) );
			yield return new WaitForSeconds(1.0f);
		}
		matchHasStarted = true;
	}

    private void IncreaseYellowScore()
    {
        yellowScore += scoringDirection * scoringRate;
        if (yellowScore <= 0) yellowScore = 0;
		HUD.UpdateYellowGauge(yellowScore);
        yellowTotem.fill();
    }

    private void IncreaseRedScore()
    {
        redScore += scoringDirection * scoringRate;
        if (redScore <= 0) redScore = 0;
		HUD.UpdateRedGauge(redScore);
        redTotem.fill();
    }

    private void IncreaseGreenScore()
    {
        greenScore += scoringDirection * scoringRate;
        if (greenScore <= 0) greenScore = 0;
		HUD.UpdateGreenGauge(greenScore);
        greenTotem.fill();
    }

    private void IncreaseBlueScore()
    {
        blueScore += scoringDirection * scoringRate;
        if (blueScore <= 0) blueScore = 0;
		HUD.UpdateBlueGauge(blueScore);
        blueTotem.fill();
    }
		
	public void InstantiatePlayers(){
		GameObject player;
		for (int i = 0; i < 4; i++) {
			player = (GameObject) Instantiate (Resources.Load ("Prefabs/Characters/"+_characterSelectController.FinalSelections[i]), playerPositions[i], Quaternion.identity);
			player.transform.Rotate (playerRotations [i]);
			player.name = _characterSelectController.FinalSelections [i];
			player.GetComponent<PlayerUserControl> ().playerNumber = i + 1;
			player.tag = "P"+(i+1);

			//Colors
			player.GetComponentInChildren<SkinnedMeshRenderer> ().material.SetColor("_OutlineColor", outlineColors[i]);
			player.GetComponent<LineRenderer>().SetColors(outlineColors[i], outlineColors[i]);
			player.GetComponent<TrailRenderer>().material = trailMat[i];

			//HUD
			player.GetComponent<PowerUp>().powerUI = GameObject.Find(powerUpHudNames[i]).GetComponent<Image>();
			player.GetComponent<Player>().sideDashHud = GameObject.Find(sideDashHudNames[i]).GetComponentsInChildren<Image>();;
		}

		HUD.SetPortraits(_characterSelectController.FinalSelections);

		greenTotem.setHead(_characterSelectController.FinalSelections[0]);
		blueTotem.setHead(_characterSelectController.FinalSelections[1]);
		yellowTotem.setHead(_characterSelectController.FinalSelections[2]);
		redTotem.setHead(_characterSelectController.FinalSelections[3]);

		Destroy(_characterSelectController.gameObject);
	}
}
