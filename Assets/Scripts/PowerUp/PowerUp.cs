using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerUp : MonoBehaviour {
	
	public Text powerUI;
	private Behaviour _playerHalo;

	public bool available {get; set;}
	public bool activated {get; set;}
	public float timestamp {get; set;}
	
	public string currentEffect;
	public static string[] pool;
	public static int count = 4;

	public AttractBall _attractBall = new AttractBall();
	private LineRenderer line;
	private GameObject _ball;

	public RepulsiveWave _repulsiveWave = new RepulsiveWave();
	public GlobalStun _globalStun = new GlobalStun();
	public Massivity _massivity = new Massivity();
	
	void Start () {
		available = false;
		activated = false;

		_playerHalo = (Behaviour) GetComponent ("Halo");
		_ball = GameObject.FindGameObjectWithTag("Ball");

		line = GetComponent<LineRenderer>();
		_attractBall.initLineRenderer(line);

		_globalStun.init(this.gameObject);

		_massivity.initScale = this.gameObject.transform.localScale;

		pool = new string[count];
		pool[0] = AttractBall.effectTag;
		pool[1] = RepulsiveWave.effectTag;
		pool[2] = GlobalStun.effectTag;
		pool[3] = Massivity.effectTag;
	}

	void Update(){
		_playerHalo.enabled = available ? true : false;
	}

	void FixedUpdate () {
		if(activated){
			switch(currentEffect){
				case "AttractBall":
					activated = _attractBall.runEffect(_ball, transform.position, timestamp, line);
					break;
				case "RepulsiveWave":
					activated = _repulsiveWave.runEffect(transform.position, transform.forward);
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
		powerUI.text = tag;
		currentEffect = tag;
	}

	public static void shufflePool(){

		string tmp;
		int rand;
		int i;

		for(i = 0; i <= count-1; i++){
			rand = Random.Range(0, count);
			tmp = pool[rand];
			pool[rand] = pool[i];
			pool[i] = tmp;
		}
	}
}
