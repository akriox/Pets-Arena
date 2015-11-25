using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	Ball ball;
	//Ball.Zone previousBallZone;

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

	private GameObject previousCylinder;

	//private GameObject redPillar;
	//private GameObject greenPillar;
	//private GameObject bluePillar;
	//private GameObject yellowPillar;

	private Material cylinderBaseMat;
	//private Material pillarBaseMat;

	private Material redMat;
	private Material greenMat;
	private Material blueMat;
	private Material yellowMat;

	public float victoryScore;
	public Text victoryUI;
	private bool matchIsOver;

	void Awake() {

		matchIsOver = false;

		GameObject ballGo = GameObject.Find ("Ball");
		ball = ballGo.GetComponent<Ball> ();
		//previousBallZone = Ball.Zone.N;

		/*
		redCylinder = GameObject.Find ("Red Cylinder");
		greenCylinder = GameObject.Find ("Green Cylinder");
		blueCylinder = GameObject.Find ("Blue Cylinder");
		yellowCylinder = GameObject.Find ("Yellow Cylinder");
		*/

		//redPillar = GameObject.Find ("Red Pillar");
		//greenPillar = GameObject.Find ("Green Pillar");
		//bluePillar = GameObject.Find ("Blue Pillar");
		//yellowPillar = GameObject.Find ("Yellow Pillar");

		cylinderBaseMat = Resources.Load ("Materials/Cylinder", typeof(Material)) as Material;
		//pillarBaseMat = Resources.Load ("Materials/Pillar", typeof(Material)) as Material;
		redMat = Resources.Load ("Materials/Red Cylinder", typeof(Material)) as Material;
	    greenMat = Resources.Load ("Materials/Green Cylinder", typeof(Material)) as Material;
	    blueMat = Resources.Load ("Materials/Blue Cylinder", typeof(Material)) as Material;
	    yellowMat = Resources.Load ("Materials/Yellow Cylinder", typeof(Material)) as Material;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		updateScore ();
		victoryCheck ();
		resetPillarColors ();
		changePillarColor ();

		if(matchIsOver && Input.GetKeyDown(KeyCode.R))
			Application.LoadLevel(Application.loadedLevel);
	}

	void updateScore(){
		if (ball.currentZone == Ball.Zone.R) {
			redScore += 0.01f;
			redScoreUI.text = redScore.ToString("0.00");
		} else if (ball.currentZone == Ball.Zone.G) {
			greenScore += 0.01f;
			greenScoreUI.text = greenScore.ToString("0.00");
		} else if (ball.currentZone == Ball.Zone.B) {
			blueScore += 0.01f;
			blueScoreUI.text = blueScore.ToString("0.00");
		} else if (ball.currentZone == Ball.Zone.Y) {
			yellowScore += 0.01f;
			yellowScoreUI.text = yellowScore.ToString("0.00");
		}
	}

	void victoryCheck(){
		if (!matchIsOver) {
			if(yellowScore > victoryScore){
				victoryUI.enabled = true;
				victoryUI.text = "J1"+victoryUI.text;
				matchIsOver = true;
			}
			else if(redScore > victoryScore){
				victoryUI.enabled = true;
				victoryUI.text = "J2"+victoryUI.text;
				matchIsOver = true;
			}
			else if(greenScore > victoryScore){
				victoryUI.enabled = true;
				victoryUI.text = "J3"+victoryUI.text;
				matchIsOver = true;
			}
			else if(blueScore > victoryScore){
				victoryUI.enabled = true;
				victoryUI.text = "J4"+victoryUI.text;
				matchIsOver = true;
			}
		}
	}

	void changePillarColor (){
		if (ball.currentZone == Ball.Zone.R) {
			redCylinder.GetComponent<Renderer> ().material = redMat;
			//redPillar.GetComponent<Renderer> ().material = redMat;
		} else if (ball.currentZone == Ball.Zone.G) {
			greenCylinder.GetComponent<Renderer> ().material = greenMat;
			//greenPillar.GetComponent<Renderer> ().material = greenMat;
		} else if (ball.currentZone == Ball.Zone.B) {
			blueCylinder.GetComponent<Renderer> ().material = blueMat;
			//bluePillar.GetComponent<Renderer> ().material = blueMat;
		} else if (ball.currentZone == Ball.Zone.Y) {
			yellowCylinder.GetComponent<Renderer>().material = yellowMat;
			//yellowPillar.GetComponent<Renderer>().material = yellowMat;
		}
	}

	void resetPillarColors(){
		if (ball.currentZone != Ball.Zone.R) {
			redCylinder.GetComponent<Renderer> ().material = cylinderBaseMat;
			//redPillar.GetComponent<Renderer> ().material = pillarBaseMat;
		}
		if (ball.currentZone != Ball.Zone.G) {
			greenCylinder.GetComponent<Renderer> ().material = cylinderBaseMat;
			//greenPillar.GetComponent<Renderer>().material = pillarBaseMat;
		}
		if (ball.currentZone != Ball.Zone.B) {
			blueCylinder.GetComponent<Renderer> ().material = cylinderBaseMat;
			//bluePillar.GetComponent<Renderer> ().material = pillarBaseMat;
		}
		if (ball.currentZone != Ball.Zone.Y) {
			yellowCylinder.GetComponent<Renderer>().material = cylinderBaseMat;
			//yellowPillar.GetComponent<Renderer>().material = pillarBaseMat;
		}
	}
}
