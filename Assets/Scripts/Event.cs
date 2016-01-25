using UnityEngine;
using System.Collections;

public class Event : MonoBehaviour {

	public float rotationSpeed;
	private Vector3 rotationVector;

	private EventController _eventController;

	public string effectTag;
	public float duration;
	
	void Start () {
		_eventController = GameObject.FindGameObjectWithTag("GameController").GetComponent<EventController>();
		rotationVector = new Vector3 (rotationSpeed, 0, 0);
	}

	void FixedUpdate(){
		transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 3F, 11F), transform.position.z);
	}

	void Update () {
		transform.Rotate (rotationVector);
		Vector3 pos = transform.position;
		pos.y =  Mathf.Clamp(transform.position.y, 2.0f, 10.0f);
		transform.position = pos;
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			if(!_eventController.activated){
				_eventController.activated = true;
				_eventController.timestamp = Time.time;

				EventController.Effect e = new EventController.Effect ();
				e.duration = duration;
				e.tag = effectTag;
				_eventController.setEffect(e);
				
				Destroy (this.gameObject);	
			}
		}
	}
}
