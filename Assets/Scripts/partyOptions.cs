using UnityEngine;
using System.Collections;

public class partyOptions : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void Update() {

		if (Input.GetAxis ("Cancel")!=0) {
			Application.LoadLevel ("charaSelect");
		}
	}
}
