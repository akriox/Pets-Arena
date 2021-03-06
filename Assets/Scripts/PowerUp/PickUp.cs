﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PickUp : MonoBehaviour {

	public float rotationSpeed;
	public float duration;
    private Vector3 rotationVector;
	private bool pickedUp;

	public string effectTag;

    private AudioSource _audioSource;

	void Start () {
		pickedUp = false;
		rotationVector = new Vector3 (0, rotationSpeed, 0);
        _audioSource = GetComponent<AudioSource>();
	}

	void FixedUpdate(){
		transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 3F, 11F), transform.position.z);
	}

	void Update () {
		transform.Rotate (rotationVector);
		Vector3 pos = transform.position;
		pos.y =  Mathf.Clamp(transform.position.y, 2.0f, 10.0f);
		transform.position = pos;

		RaycastHit hitInfo;
		if (Physics.Raycast(transform.position, Vector3.down, out hitInfo))
		{
			if(hitInfo.collider.tag == "Obstacle"){
				transform.Translate(new Vector3(2f, 0f, 2f));
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == 11 && !pickedUp) {
			PowerUp p = other.GetComponent<PowerUp>();
			if(!p.available && !p.activated){
				_audioSource.Play();
				pickedUp = true;
                p.available = true;
				p.setEffect(effectTag);
                GetComponent<MeshRenderer>().enabled = false; // hide mesh before destroying
				Destroy (this.gameObject, _audioSource.clip.length); // wait end of clip to destroy
			}
		}
	}
}