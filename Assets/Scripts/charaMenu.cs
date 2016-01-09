using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class charaMenu : MonoBehaviour {

	public Canvas charaCanvas;
    public Button axolotl;
	public Button fennec;
	public Button kommodo;
	public Button otter;
	public Button startMap;
	public Button partyOptions;
	public GameObject evenement;

    // Use this for initialization
    void Start () {
        charaCanvas = charaCanvas.GetComponent<Canvas>();
		axolotl = axolotl.GetComponent<Button>();
		fennec = fennec.GetComponent<Button>();
		kommodo = kommodo.GetComponent<Button>();
		otter = otter.GetComponent<Button>();
		startMap = startMap.GetComponent<Button> ();
		partyOptions = partyOptions.GetComponent<Button>();
    }

	public void LoadScene (int level) {
		Application.LoadLevel (level);
	}

	void Update() {

		if (Input.GetAxis ("Cancel")!=0) {
			Application.LoadLevel ("main");
		}

		if (Input.GetAxis ("Submit")!=0) {
			evenement.SetActive (true);
		}


	}
}