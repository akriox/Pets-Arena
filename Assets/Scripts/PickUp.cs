﻿using UnityEngine;
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
}
