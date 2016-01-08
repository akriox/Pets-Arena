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

	void Update () {
		transform.Rotate (rotationVector);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			PowerUp p = other.GetComponent<PowerUp>();
			if(!p.available){
				p.available = true;
				p.setEffect(this.effectTag);
				Destroy (this.gameObject);	
			}
		}
	}
}