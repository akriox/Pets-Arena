using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class mapMenu : MonoBehaviour {

	public Canvas mapCanvas;
    public Button articText;
	public Button desertText;
	public Button jungleText;

    // Use this for initialization
    void Start () {
        mapCanvas = mapCanvas.GetComponent<Canvas>();
		articText = articText.GetComponent<Button>();
		desertText = desertText.GetComponent<Button>();
		jungleText = jungleText.GetComponent<Button>();
    }

	public void LoadScene (int level) {
		Application.LoadLevel (level);
	}

	public void LaunchAnimation (Animator anim) {
		anim.GetComponent<Animator> ().Play (0);
	}

	void Update() {

		if (Input.GetAxis ("Cancel")!=0) {
			Application.LoadLevel ("charaSelect");
		}
	}
}