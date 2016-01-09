using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class splashMenu : MonoBehaviour {
    void Start () {

    }

	void Update() {
		
		if (Input.GetAxis ("Submit")!=0) {
			Application.LoadLevel ("main");
		}
	}


	public void LoadScene (int level) {
		Application.LoadLevel (level);
	}
}