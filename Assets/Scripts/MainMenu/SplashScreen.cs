using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

    public GameObject mainPanel;

	void Update () {
        if (Input.anyKey){
            mainPanel.SetActive(true);
            this.gameObject.SetActive(false);
        }
	}
}
