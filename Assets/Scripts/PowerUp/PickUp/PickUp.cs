using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	public float rotationSpeed;
	private Vector3 rotationVector;
	public float duration;

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

				/*
				 * TO DO : effect set randomly
				*/

				//p.setEffect(RepulsiveWave.effectTag);
				//p.setEffect(AttractBall.effectTag);
				p.setEffect(GlobalStun.effectTag);
				//p.setEffect(Massivity.effectTag);

				Destroy (this.gameObject);	
			}
		}
	}
}