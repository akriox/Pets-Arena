using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeadUpDisplay {

	private string spritesPath = "2D/HUD/Gauges/hudGaugeFinale_";

	private Text message;
	private Image spriteMessage;

	private string[] playerColors = { "Green", "Blue", "Yellow", "Red" };

	///<summary>
	///	[0]: Green, [1]: Blue, [2]: Yellow, [3]: Red
	///</summary>
	private GameObject[] playerHUD;

	///<summary>
	///	[0]: Green, [1]: Blue, [2]: Yellow, [3]: Red
	///</summary>
	private Image[] playerGaugeGlow;

	///<summary>
	///	[0]: Green, [1]: Blue, [2]: Yellow, [3]: Red
	///</summary>
	private Image[] playerScoreGauge;


	///<summary>
	///	[0][]: Green, [1][]: Blue, [2][]: Yellow, [3][]: Red
	///</summary>
	private Sprite[][] playerGaugeSprites;

	///<summary>
	///	[0][]: Green, [1][]: Blue, [2][]: Yellow, [3][]: Red
	///</summary>
	private static Image[][] playerSideDash;

	///<summary>
	///	[0]: Green, [1]: Blue, [2]: Yellow, [3]: Red
	///</summary>
	public Sprite[] winnerSprite;

	///<summary>
	///	[0][]: Green, [1][]: Blue, [2][]: Yellow, [3][]: Red
	///</summary>
	private Sprite[][] playerPortraitSprites;

	private string portraitSpritesPath = "2D/HUD/Portraits/";
	private Image[] portraitImages;

	private float gaugeUnit;

	public void Init(){
		message = GameObject.FindGameObjectWithTag("MessageText").GetComponent<Text>();
		spriteMessage = GameObject.FindGameObjectWithTag("MessageSprite").GetComponent<Image>();

		winnerSprite = Resources.LoadAll<Sprite>("2D/HUD/Winner");

		gaugeUnit = GameController.victoryScore / 25.0f;

		playerHUD = new GameObject[4];
		playerScoreGauge = new Image[4];
		playerGaugeGlow = new Image[4];
		playerSideDash = new Image[4][];
		playerGaugeSprites = new Sprite[4][];
		playerPortraitSprites = new Sprite[4][];
		portraitImages = new Image[4];

		for(int i = 0; i < 4; i++){
			playerHUD[i] = GameObject.FindGameObjectWithTag(playerColors[i] + "PlayerHUD");
			portraitImages[i] = playerHUD[i].transform.Find(playerColors[i] + "Portrait").GetComponent<Image>();
			playerScoreGauge[i] = playerHUD[i].transform.Find(playerColors[i] + "ScoreGauge").GetComponent<Image>();
			playerGaugeGlow[i] = playerHUD[i].transform.Find(playerColors[i] + "ScoreGaugeGlow").GetComponent<Image>();
			playerSideDash[i] = playerHUD[i].transform.Find(playerColors[i] + "SideDash").GetComponentsInChildren<Image>();
			playerPortraitSprites[i] = Resources.LoadAll<Sprite>(portraitSpritesPath + playerColors[i]);
			playerGaugeSprites[i] = Resources.LoadAll<Sprite>(spritesPath + playerColors[i]);
		}
	}

	public void UpdatePlayerGauge(int playerIndex, float score){
		playerScoreGauge[playerIndex].sprite = playerGaugeSprites[playerIndex][(int)(score/gaugeUnit)];
		UpdateGaugesGlows(playerIndex);
	}
		
	public void UpdateGaugesGlows(int playerIndex){
		for(int i = 0; i < 4; i++){
			playerGaugeGlow[i].enabled = false;
		}
		if(playerIndex != -1)
			playerGaugeGlow[playerIndex].enabled = true;
	}

	public static void UpdateSideDash(int playerIndex, int chargeCount){
		playerSideDash[playerIndex-1][0].color = chargeCount > 0 ? Color.white : Color.clear;
		playerSideDash[playerIndex-1][1].color = chargeCount > 1 ? Color.white : Color.clear;
	}

	public void DisplayMessage(string msg){
		message.enabled = true;
		message.text = msg;
	}

	public IEnumerator DisplayMessage(string msg, float duration){
		message.text = msg;
		message.enabled = true;
		yield return new WaitForSeconds(duration);
		message.text = "";
		message.enabled = false;
	}

	public void DisplaySprite(Sprite s){
		spriteMessage.color = Color.white;
		spriteMessage.sprite = s;
	}

	public IEnumerator DisplaySprite(Sprite s, float duration){
		spriteMessage.sprite = s;
		spriteMessage.color = Color.white;
		yield return new WaitForSeconds(duration);
		spriteMessage.color = Color.clear;
	}

	public void SetPortrait(int playerIndex, string animalName){
		int index = -1;
		switch(animalName){
			case "Axolotl": index = 0; break;
			case "Fennec": index = 1; break;
			case "Leopard": index = 2; break;
			case "Loutre": index = 3; break;
		}
		portraitImages[playerIndex-1].sprite = playerPortraitSprites[playerIndex-1][index];
	}
}
