﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Winner : MonoBehaviour {

	#pragma warning disable 219
	public string animalName;
	private bool sceneLoaded = false;

	void Awake(){
		DontDestroyOnLoad(this.gameObject);
	}

	void Update(){
		int playerCount = GamepadInput.Instance.gamepads.Count;
		for(int i = 0; i < playerCount; i++){
			if(sceneLoaded){
				if( GamepadInput.Instance.gamepads[i].GetButtonDown(GamepadButton.Action2)
				|| GamepadInput.Instance.gamepads[i].GetButtonDown(GamepadButton.Back)
				|| GamepadInput.Instance.gamepads[i].GetButtonDown(GamepadButton.Start) )
				{
					SceneManager.LoadScene("CharacterSelect");
				}
			}
		}
	}

	void OnLevelWasLoaded(int loadedLevel){
		if(loadedLevel == 3){
			GameObject animal = (GameObject) Instantiate(Resources.Load("Prefabs/WinnerCharacters/" + animalName));
			sceneLoaded = true;
			StartCoroutine(LoadCharSelectScene());
		}
	}

	private IEnumerator LoadCharSelectScene(){
		yield return new WaitForSeconds(15.0f);
		SceneManager.LoadScene("CharacterSelect");
	}
}