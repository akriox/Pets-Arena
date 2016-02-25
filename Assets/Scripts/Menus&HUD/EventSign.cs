using UnityEngine;
using System.Collections;

public class EventSign : MonoBehaviour {

	public bool DroppingDown = false;
	public bool PullingUp = true;
	private bool willWiggle = false;

	public Vector3 DownPos;
	public Vector3 UpPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (DroppingDown || PullingUp)
			Animate ();
	}

	public void ChangeTexture(string evt){
		gameObject.GetComponent<Renderer> ().material = (Material) Resources.Load ("Materials/Events/WoodSign/wood_sign_diffuse_" + evt);
	}

	public void ToggleAnimation(){
		DroppingDown = !DroppingDown;
		PullingUp = !PullingUp;
		willWiggle = !willWiggle;
		GetComponent<Animator> ().SetBool ("Wiggle", willWiggle);
	}

	void Animate(){
		Vector3 tmp_vector = transform.position;
		if (PullingUp)
			transform.position = Vector3.Lerp (tmp_vector, UpPos, Time.deltaTime * 3);
		if (DroppingDown) {
			transform.position = Vector3.Lerp (tmp_vector, DownPos, Time.deltaTime * 3); 
		}
	}
}
