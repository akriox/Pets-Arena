﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterSelectMenu : MonoBehaviour {

	public Image[] playerStateImg;
	public Image howToPlayImg;

	private Sprite[] player1Sprites;
	private Sprite[] player2Sprites;
	private Sprite[] player3Sprites;
	private Sprite[] player4Sprites;

	void Start () {	
		player1Sprites = Resources.LoadAll<Sprite>("2D/CharacterSelect/Player1");
		player2Sprites = Resources.LoadAll<Sprite>("2D/CharacterSelect/Player2");
		player3Sprites = Resources.LoadAll<Sprite>("2D/CharacterSelect/Player3");
		player4Sprites = Resources.LoadAll<Sprite>("2D/CharacterSelect/Player4");
	}

	void Update () {
		for(int i = 0; i < GamepadInput.Instance.gamepads.Count; i++){
			if(GamepadInput.Instance.gamepads[i].GetButtonDown(GamepadButton.Action4)){
				howToPlayImg.enabled = !howToPlayImg.enabled;
			}
			if(GamepadInput.Instance.gamepads[i].GetButtonDown(GamepadButton.Action2)){
				howToPlayImg.enabled = false;
			}
		}
		if(Input.GetKeyDown(KeyCode.Escape))
			SceneManager.LoadScene("MainMenu");
	}

	public void UpdatePlayerStateSprite(int PlayerIndex, int SpriteIndex){
		switch(PlayerIndex){
			case 0: playerStateImg[PlayerIndex].sprite = player1Sprites[SpriteIndex]; break;
			case 1: playerStateImg[PlayerIndex].sprite = player2Sprites[SpriteIndex]; break;
			case 2: playerStateImg[PlayerIndex].sprite = player3Sprites[SpriteIndex]; break;
			case 3: playerStateImg[PlayerIndex].sprite = player4Sprites[SpriteIndex]; break;
		}
	}
}
