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

	private GameObject _ball;
	private BallController _ballController;
	private GameController _gameController;

	// Use this for initialization
	void Start () {
		activated = false;
		
		_ball = GameObject.FindGameObjectWithTag("Ball");
		_ballController = _ball.GetComponent<BallController>();

		_gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		
		trapBall.tag = "TrapBall";
		trapBall.duration = 8.0f;
		
		bouncyBall.tag = "BouncyBall";
		bouncyBall.duration = 8.0f;
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
			}
		}
	}

	void TrapBall(){
		if (Time.time - this.timestamp <= trapBall.duration) {
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
		if (Time.time - this.timestamp <= trapBall.duration) {
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

	public void setEffect(Effect e){
		currentEffect = e;
		eventUI.text = e.tag;
	}
}
