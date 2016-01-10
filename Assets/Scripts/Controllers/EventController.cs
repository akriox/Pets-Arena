using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventController : MonoBehaviour {

	public Text eventUI;

	public bool activated { get; set; }
	public float timestamp {get; set;}

	public struct Effect {
		public string tag;
		public float duration;
	}
	
	public Effect currentEffect;
	
	public static Effect trapBall;
	public static Effect bouncyBall;
	public static Effect stretchBall;

	private GameObject _ball;
	public Vector3 initScale {get; set;}
	private float scaleFactor = 0.0f;
	private float speed = 5.0f;

	private BallController _ballController;
	private GameController _gameController;

	public static string[] pool;
	public static int count = 4;

	// Use this for initialization
	void Start () {
		activated = false;
		
		_ball = GameObject.FindGameObjectWithTag("Ball");
		_ballController = _ball.GetComponent<BallController>();
		initScale = _ball.transform.localScale;
		_gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

		pool = new string[count];
		pool[0] = "TrapBall";
		pool[1] = "BouncyBall";
		pool[2] = "StretchBall";
		pool[3] = "SwitchedZones";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		if (activated) {
			switch (currentEffect.tag) {
			case "TrapBall":
				TrapBall ();
				break;
			case "BouncyBall":
				BouncyBall ();
				break;
			case "StretchBall":
				StretchBall();
				break;
			case "SwitchedZones":
				SwitchedZones();
				break;
			}
		}
	}

	void TrapBall(){
		if (Time.time - this.timestamp <= currentEffect.duration) {
			eventUI.enabled = true;
			_ballController.trapped = true;
			_gameController.scoringDirection = -1;
		} else {
			eventUI.enabled = false;
			_ballController.trapped = false;
			_gameController.scoringDirection = 1;
			this.activated = false;
		}
	}

	void BouncyBall(){
		if (Time.time - this.timestamp <= currentEffect.duration) {
			eventUI.enabled = true;
			_ballController.bouncy = true;
			_ballController.bouncePower *= 2f;
			_ball.GetComponent<Collider>().material.bounciness = 1f;
			_ball.GetComponent<Collider>().material.staticFriction = 0.3f;
			_ball.GetComponent<Collider>().material.dynamicFriction = 0.3f;
			_ball.GetComponent<Collider>().material.frictionCombine = PhysicMaterialCombine.Average;
		} else {
			eventUI.enabled = false;
			_ballController.bouncy = false;
			_ballController.bouncePower *= 0.5f;
			_ball.GetComponent<Collider>().material.bounciness = 0.5f;
			_ball.GetComponent<Collider>().material.staticFriction = 0.6f;
			_ball.GetComponent<Collider>().material.dynamicFriction = 0.6f;
			_ball.GetComponent<Collider>().material.frictionCombine = PhysicMaterialCombine.Minimum;
			this.activated = false;
		}
	}

	void StretchBall() {
		if(Time.time - timestamp <= currentEffect.duration){
			eventUI.enabled = true;
			scaleFactor = Mathf.Clamp01(scaleFactor + speed * Time.deltaTime);
			Vector3 stretchScale = new Vector3(initScale.x, initScale.y*1.5f, initScale.z);
			_ball.transform.localScale = Vector3.Slerp(initScale, stretchScale, scaleFactor);
		}
		else{
			eventUI.enabled = false;
			_ball.transform.localScale = initScale;
			this.activated = false;
		}
	}

	void SwitchedZones(){
		if(Time.time - timestamp <= currentEffect.duration){
			eventUI.enabled = true;
			_gameController.switchedZones = true;
		}
		else{
			eventUI.enabled = false;
			_gameController.switchedZones = false;
			this.activated = false;
		}
	}

	public void setEffect(Effect e){
		currentEffect = e;
		eventUI.text = e.tag;
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
