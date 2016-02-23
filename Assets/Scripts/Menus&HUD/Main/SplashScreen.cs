using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreen : MonoBehaviour {

    public GameObject mainPanel;

	void Update () {
        if (Input.anyKey){
			/*
            mainPanel.SetActive(true);
            this.gameObject.SetActive(false);
            */
			SceneManager.LoadScene("CharacterSelect");
        }
	}
}
