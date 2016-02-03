using UnityEngine;
using System.Collections;

public class FakeBall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player") {
			Player p = col.gameObject.GetComponent<Player>();
			if(p.dashing){
				Destroy(this.gameObject);
			}
		}
	}
}
