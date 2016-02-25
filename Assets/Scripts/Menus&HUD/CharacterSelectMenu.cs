using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterSelectMenu : MonoBehaviour {

	public Image[] playerStateImg;
	public Image quitPopUpImg;
	public Image howToPlayImg;
	public Image pressStartImg;

	private string spritePath = "2D/CharacterSelectMenu/";
	private Sprite[] player1Sprites;
	private Sprite[] player2Sprites;
	private Sprite[] player3Sprites;
	private Sprite[] player4Sprites;

	void Start () {	
		pressStartImg.enabled = false;
		howToPlayImg.enabled = false;
		quitPopUpImg.enabled = false;
		player1Sprites = Resources.LoadAll<Sprite>(spritePath + "Player1");
		player2Sprites = Resources.LoadAll<Sprite>(spritePath + "Player2");
		player3Sprites = Resources.LoadAll<Sprite>(spritePath + "Player3");
		player4Sprites = Resources.LoadAll<Sprite>(spritePath + "Player4");
	}

	void Update () {
		if(howToPlayImg != null && quitPopUpImg != null){
			for(int i = 0; i < GamepadInput.Instance.gamepads.Count; i++){
				if(GamepadInput.Instance.gamepads[i].GetButtonDown(GamepadButton.Action4)){
					howToPlayImg.enabled = !howToPlayImg.enabled;
				}
				if(GamepadInput.Instance.gamepads[i].GetButtonDown(GamepadButton.Action2)){
					howToPlayImg.enabled = false;
				}
					
				if(GamepadInput.Instance.gamepads[i].GetButtonDown(GamepadButton.Back)){
					quitPopUpImg.enabled = !quitPopUpImg.enabled;
				}

				if(quitPopUpImg.enabled){
					if(GamepadInput.Instance.gamepads[i].GetButtonDown(GamepadButton.Action1)){
						Application.Quit();
					}
					if(GamepadInput.Instance.gamepads[i].GetButtonDown(GamepadButton.Action2)){
						quitPopUpImg.enabled = false;
					}
				}
			}
		}
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
