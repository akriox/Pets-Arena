using UnityEngine;
using System.Collections;

public class Event : MonoBehaviour {

	public float rotation = -90.0f;
	private Vector3 rotationVector;

	private EventController _eventController;

	private EventSign _woodSign;

	public string effectTag;
	public float duration;
	
	void Start () {
		_eventController = GameObject.FindGameObjectWithTag("GameController").GetComponent<EventController>();
		_woodSign = GameObject.Find ("EventSign").GetComponent<EventSign>();
		rotationVector = new Vector3 (rotation, 0, 0);
		transform.Rotate (rotationVector);
	}

	void FixedUpdate(){
		transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 3F, 11F), transform.position.z);
	}

	void Update () {
		Vector3 pos = transform.position;
		pos.y =  Mathf.Clamp(transform.position.y, 2.0f, 10.0f);
		transform.position = pos;
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == 11) {
			if(!_eventController.activated){
				_eventController.activated = true;
				_eventController.timestamp = Time.time;
				EventController.Effect e = new EventController.Effect ();
				e.duration = duration;
				e.tag = effectTag;
				_woodSign.ChangeTexture (effectTag);
				_woodSign.ToggleAnimation ();
				_eventController.setEffect(e);
				
				Destroy (this.gameObject);	
			}
		}
	}
}
