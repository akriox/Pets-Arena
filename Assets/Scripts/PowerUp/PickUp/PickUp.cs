using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	public float rotationSpeed;
	public float duration;
	private Vector3 rotationVector;
	private int poolIndex;

	public string effectTag;

	void Start () {
		rotationVector = new Vector3 (0, rotationSpeed, 0);
		poolIndex = Random.Range(0, PowerUp.count-1);
		PowerUp.shufflePool();
		effectTag = PowerUp.pool[poolIndex];
	}

	void FixedUpdate(){
		transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 3F, 11F), transform.position.z);
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