using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public bool debugging;

	private GameObject[] players;

	private BallController _ballController;
	private HeadUpDisplay HUD;
	private Sprite[] countDownSprites;

	private bool loadingWinScene;
	private int winnerIndex = -1;
	public float redScore { get; set; }
    public float greenScore { get; set; }
    public float blueScore { get; set; }
    public float yellowScore { get; set; }

	/// <summary>
	/// [0]: GreenTotem, [1]: BlueTotem, [2]: YellowTotem, [3]: RedTotem
	/// </summary>
	public Totem[] totems;
	public List<GameObject> Runes;

	public Image PauseMenu;

	private string characterPrefabPath = "Prefabs/GameCharacters/";
	private string[] powerUpHudNames = {"GreenPower", "BluePower", "YellowPower", "RedPower"};
	private Color[] outlineColors = { new Color(58f/255f, 1f, 32f/255f), new Color(6f/255f, 193f/255f, 1f), new Color(234f/255f, 209f/255f, 0f), new Color(1f, 6f/255f, 6f/255f) };
	private Material[] trailMat;

	private CharacterSelectController _characterSelectController;
	private List<Vector3> playerPositions = new List<Vector3> ();
	private List<Vector3> playerRotations = new List<Vector3> ();

	public static bool matchHasStarted;
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
		matchHasStarted = false;
		loadingWinScene = false;
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

		if(!debugging){
			_characterSelectController = GameObject.Find("Character Select Controller").GetComponent<CharacterSelectController>();
			players = new GameObject[4];
			InstantiatePlayers ();
		}
	}

	void Start(){
		StartCoroutine (Countdown ());
	}

	void Update(){
		if (!matchIsOver){
			if(Time.timeScale > 0f) updateScore ();
			victoryCheck ();
		}
		else{
			if(!loadingWinScene){
				GetComponent<AudioSource>().Play();
				loadingWinScene = true;
				StartCoroutine(LoadWinScene());	
			}
		}

		int GamepadCount = GamepadInput.Instance.gamepads.Count;

		for (int i = 0; i < GamepadCount; i++) {
			if (GamepadInput.Instance.gamepads [i].GetButtonDown (GamepadButton.Start))
				PauseMenu.enabled = togglePause ();
			if (PauseMenu.enabled && GamepadInput.Instance.gamepads [i].GetButtonDown (GamepadButton.Back)) {
				togglePause ();
				SceneManager.LoadScene ("CharacterSelect");
			}
		}
	}
		
	bool togglePause()
	{
		if(Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
			return(false);
		}
		else
		{
			Time.timeScale = 0f;
			return(true);    
		}
	}
		
	private void victoryCheck(){
		if(greenScore >= victoryScore){
			HUD.DisplaySprite(HUD.winnerSprite[0]);
			matchIsOver = true;
			winnerIndex = 0;
		}
		else if(blueScore >= victoryScore){
			HUD.DisplaySprite(HUD.winnerSprite[1]);
			matchIsOver = true;
			winnerIndex = 1;
		}
		else if(yellowScore >= victoryScore){
			HUD.DisplaySprite(HUD.winnerSprite[2]);
			matchIsOver = true;
			winnerIndex = 2;
		}
		else if(redScore >= victoryScore){
			HUD.DisplaySprite(HUD.winnerSprite[3]);
			matchIsOver = true;
			winnerIndex = 3;
		}
	}

	private void updateScore (){
		if(switchedZones) hideRunes();
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
				HUD.UpdateGaugesGlows (-1);
				hideRunes ();
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

    private void IncreaseGreenScore()
    {
        greenScore += scoringDirection * scoringRate;
        if (greenScore <= 0) greenScore = 0;
		HUD.UpdatePlayerGauge(0, greenScore);
		totems[0].fill();
		if (!Runes[0].activeSelf)
			Runes [0].SetActive (true);
    }

    private void IncreaseBlueScore()
    {
        blueScore += scoringDirection * scoringRate;
        if (blueScore <= 0) blueScore = 0;
		HUD.UpdatePlayerGauge(1, blueScore);
		totems[1].fill();
		if (!Runes[1].activeSelf)
			Runes [1].SetActive (true);
    }

	private void IncreaseYellowScore()
	{
		yellowScore += scoringDirection * scoringRate;
		if (yellowScore <= 0) yellowScore = 0;
		HUD.UpdatePlayerGauge(2, yellowScore);
		totems[2].fill();
		if (!Runes[2].activeSelf)
			Runes [2].SetActive (true);
	}

	private void IncreaseRedScore()
	{
		redScore += scoringDirection * scoringRate;
		if (redScore <= 0) redScore = 0;
		HUD.UpdatePlayerGauge(3, redScore);
		totems[3].fill();
		if (!Runes[3].activeSelf)
			Runes [3].SetActive (true);
	}

	private void hideRunes(){
		for (int i = 0; i < 4; i++) {
			if(Runes[i].activeSelf)
				Runes [i].SetActive (false);
		}
	}
	private void InstantiatePlayers(){
		string animalName;
		for (int i = 0; i < 4; i++) {
			animalName = _characterSelectController.FinalSelections[i];

			players[i] = (GameObject) Instantiate (Resources.Load (characterPrefabPath + animalName), playerPositions[i], Quaternion.identity);
			players[i].transform.Rotate (playerRotations [i]);
			players[i].name = animalName;
			players[i].GetComponent<PlayerUserControl> ().playerNumber = i + 1;
			players[i].tag = "P" + (i+1);

			//Colors
			players[i].GetComponentInChildren<SkinnedMeshRenderer> ().material.SetColor("_OutlineColor", outlineColors[i]);
			players[i].GetComponent<LineRenderer>().SetColors(outlineColors[i], outlineColors[i]);
			players[i].GetComponent<TrailRenderer>().material = trailMat[i];
			players[i].GetComponentInChildren<PowerUpHalo>().setColor(i+1);

			//HUD
			players[i].GetComponent<PowerUp>().powerUpImg = GameObject.Find(powerUpHudNames[i]).GetComponent<Image>();
			HUD.SetPortrait(i+1, animalName);

			//Totem
			totems[i].setHead(animalName);
		}

		Destroy(_characterSelectController.gameObject);
	}

	private IEnumerator LoadWinScene(){
		GameObject winner = (GameObject) Instantiate(Resources.Load ("Prefabs/Winner"), Vector3.zero, Quaternion.identity);
		winner.name = "Winner";
		winner.GetComponent<Winner>().animalName = players[winnerIndex].name;
		yield return new WaitForSeconds(5.0f);
		SceneManager.LoadScene("Win");
	}
}
