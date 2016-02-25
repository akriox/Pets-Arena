using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Winner : MonoBehaviour {

	public string animalName;
	private bool sceneLoaded = false;

	void Awake(){
		DontDestroyOnLoad(this.gameObject);
	}

	void Update(){
		for(int i = 0; i < 4; i++){
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
		yield return new WaitForSeconds(10.0f);
		SceneManager.LoadScene("CharacterSelect");
	}
}