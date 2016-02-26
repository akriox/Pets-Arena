using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerUp : MonoBehaviour {
	
	public Image powerUpImg;

	private GameObject _powerHalo;

	public bool available {get; set;}
	public bool activated {get; set;}
	public float timestamp {get; set;}
	
	public string currentEffect;
	public static int count = 4;

	public AttractBall _attractBall = new AttractBall();
	private LineRenderer line;
	private GameObject _ball;

	public RepulsiveWave _repulsiveWave = new RepulsiveWave();
	public GlobalStun _globalStun = new GlobalStun();
	public Massivity _massivity = new Massivity();

	private Sprite attractBallSprite;
	private Sprite repulsiveWaveSprite;
	private Sprite globalStunSprite;
	private Sprite massivitySprite;
	
	void Start () {
		available = false;
		activated = false;

		_powerHalo = transform.Find ("PowerUpHalo").gameObject;

		_ball = GameObject.FindGameObjectWithTag("Ball");

		line = GetComponent<LineRenderer>();
		_attractBall.initLineRenderer(line);
		attractBallSprite = Resources.Load<Sprite>("2D/HUD/PowerUp/AttractBall");

		_globalStun.init(this.gameObject);
		globalStunSprite = Resources.Load<Sprite>("2D/HUD/PowerUp/GlobalStun");

		_massivity.initScale = this.gameObject.transform.localScale;
		massivitySprite = Resources.Load<Sprite>("2D/HUD/PowerUp/Massivity");

		repulsiveWaveSprite = Resources.Load<Sprite>("2D/HUD/PowerUp/RepulsiveWave");
	}

	void Update(){
		_powerHalo.SetActive (available);
	}

	void FixedUpdate () {
		if(activated){
			switch(currentEffect){
				case "AttractBall":
					activated = _attractBall.runEffect(_ball, transform.position, timestamp, line);
					break;
				case "RepulsiveWave":
					StartCoroutine(RepulsiveWaveParticles());
					activated = _repulsiveWave.runEffect(this.gameObject, transform.position, transform.forward);
					break;
				case "GlobalStun":
					activated = _globalStun.runEffect();
					break;
				case "Massivity":
					activated = _massivity.runEffect(this.gameObject, timestamp);
					break;
			}
		}
	}

	public void setEffect(string tag){
		switch(tag){
			case "AttractBall":
				powerUpImg.sprite = attractBallSprite;
				break;
			case "RepulsiveWave":
				powerUpImg.sprite = repulsiveWaveSprite;
				break;
			case "GlobalStun":
				powerUpImg.sprite = globalStunSprite;
				break;
			case "Massivity":
				powerUpImg.sprite = massivitySprite;
				break;
		}
		powerUpImg.color = Color.white;
		currentEffect = tag;
	}

	private IEnumerator RepulsiveWaveParticles(){
		GameObject go = (GameObject) Instantiate(Resources.Load("Prefabs/RepulsiveWaveParticles"), this.transform.position, Quaternion.identity);
		go.GetComponent<ParticleSystem>().playbackSpeed = 5f;
		yield return new WaitForSeconds(1.0f);
		Destroy(go);
	}
}
