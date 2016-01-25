using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	public float rotationSpeed;
	public float duration;
    private Vector3 rotationVector;

	public string effectTag;

	void Start () {
		rotationVector = new Vector3 (0, rotationSpeed, 0);
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
			PowerUp p = other.GetComponent<PowerUp>();
			if(!p.available && !p.activated){
				p.available = true;
                p.setEffect(this.effectTag);
				Destroy (this.gameObject);	
			}
		}
	}
}