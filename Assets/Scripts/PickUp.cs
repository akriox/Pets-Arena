using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	public float rotationSpeed;
	private Vector3 rotationVector;
	public float duration;

	// Use this for initialization
	void Start () {
		rotationVector = new Vector3 (0, rotationSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (rotationVector);
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "Player") {
			Player p = col.gameObject.GetComponent<Player>();
			if(!p.hasItem){
				p.hasItem = true;
				p.itemDuration = duration;
				Destroy (gameObject);	
			}
		}
	}
}
