using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class optionsMenu : MonoBehaviour {

    public Canvas optionsCanvas;
	public Button backText;
	public Toggle fullscreen;
	public Dropdown resolution;
	
	// Use this for initialization
    void Start () {
        optionsCanvas = optionsCanvas.GetComponent<Canvas>();
		backText = backText.GetComponent<Button>();
		fullscreen = fullscreen.GetComponent<Toggle> ();
		resolution = resolution.GetComponent<Dropdown> ();

		if (Screen.fullScreen == true) {
			fullscreen.isOn = true;
		} 
		else {
			fullscreen.isOn = false;
		}
    }

	public void LoadScene (int level) {
		Application.LoadLevel (level);
	}

	public void toggleFullscreen (bool fullscreen) {
		Screen.fullScreen = !Screen.fullScreen;
	}

	void Update() {
		if (Input.GetAxis ("Cancel")!=0) {
			Application.LoadLevel ("main");
		}

		if (resolution.value==1) {
			Screen.SetResolution (1920, 1050, true);
		}

		if (resolution.value==2) {
			Screen.SetResolution (1680, 1050, true);
		}

		if (resolution.value==3) {
			Screen.SetResolution (1280, 1024, true);
		}



	}
}