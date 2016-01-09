using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class helpMenu : MonoBehaviour {

	public Button quitText;

	void Start () {
		quitText = quitText.GetComponent<Button>();
	}

	public void LoadScene (int level) {
		Application.LoadLevel (level);
	}

	void Update() {
		if (Input.GetAxis ("Cancel")!=0) {
			Application.LoadLevel ("main");
		}
	}
}
