using UnityEngine;
using System.Collections;

public class Event : MonoBehaviour {

	public float rotationSpeed;
	private Vector3 rotationVector;

	private EventController _eventController;

	void Start () {
		_eventController = GameObject.FindGameObjectWithTag("GameController").GetComponent<EventController>();

		rotationVector = new Vector3 (rotationSpeed, 0, 0);
	}
	
	void Update () {
		transform.Rotate (rotationVector);
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			PowerUp p = other.GetComponent<PowerUp>();
			if(!_eventController.activated){
				_eventController.activated = true;
				_eventController.timestamp = Time.time;
				_eventController.setEffect(EventController.trapBall);
				
				Destroy (this.gameObject);	
			}
		}
	}
}
