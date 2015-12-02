using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerUp : MonoBehaviour {
	
	public Text powerUI;

	public bool available {get; set;}
	public bool activated {get; set;}
	public float timestamp {get; set;}

	public struct Effect {
		public string tag;
		public float duration;
	}

	public Effect currentEffect;

	public static Effect attractBall;
	public static Effect spikeBall;
	
	private GameObject _ball;
	private BallController _ballController;
	private Behaviour _playerHalo;

	private LineRenderer line;
	private float gravityPull = 20.0f;

	void Start () {
		available = false;
		activated = false;

		_ball = GameObject.FindGameObjectWithTag("Ball");
		_ballController = _ball.GetComponent<BallController>();
		_playerHalo = (Behaviour) GetComponent ("Halo");
		initLineRenderer();

		attractBall.tag = "AttractBall";
		attractBall.duration = 2.0f;

		spikeBall.tag = "SpikeBall";
		spikeBall.duration = 2.0f;
	}

	void Update(){
		_playerHalo.enabled = available ? true : false;
	}

	void FixedUpdate () {
		if(activated){
			switch(currentEffect.tag){
				case "AttractBall":
					AttractBall();
					break;
				case "SpikeBall":
					SpikeBall();
					break;
			}
		}
	}

	public void setEffect(Effect e){
		currentEffect = e;
		powerUI.text = e.tag;
	}

	private void AttractBall(){
		if(Time.time - this.timestamp <= attractBall.duration){
			_ball.transform.LookAt (transform.position);
			_ball.GetComponent<Rigidbody>().AddForce(_ball.transform.TransformDirection(Vector3.forward).normalized * gravityPull, ForceMode.Acceleration);
			updateLineRenderer(true);
		}
		else{
			this.activated = false;
			updateLineRenderer(false);
		}
	}

	private void SpikeBall(){
		if(Time.time - this.timestamp <= spikeBall.duration){
			_ballController.spiked = true;
		}
		else{
			_ballController.spiked = false;
			this.activated = false;
		}
	}
	
	private void initLineRenderer(){
		line = GetComponent<LineRenderer>();
		line.SetVertexCount(2);
		line.SetWidth(2.5f, 2.5f);
		line.enabled = false;
	}
	
	private void updateLineRenderer(bool value){
		line.enabled = value;
		line.SetPosition(0, transform.position);
		line.SetPosition(1, _ball.transform.position);
	}
}
