using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class mainMenu : MonoBehaviour {
	
    public Button startText;
    public Button optionsText;
    public Button exitText;

    void Start () {
		startText = startText.GetComponent<Button>();
		optionsText = optionsText.GetComponent<Button>();
		exitText = exitText.GetComponent<Button>();
    }

	public void LoadScene (int level) {
		Application.LoadLevel (level);
	}

	void Update() {
		if (Input.GetAxis ("Cancel")!=0) {
			Application.LoadLevel ("splash");
		}
	}
}