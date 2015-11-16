using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	public float rotationSpeed;
	private Vector3 rotationVector;

	// Use this for initialization
	void Start () {
		rotationVector = new Vector3 (0, rotationSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (rotationVector);
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "Player") {
			Player p = col.gameObject.GetComponent<Player>();
			p.hasItem = true;
			Destroy (gameObject);
		}
	}
}
