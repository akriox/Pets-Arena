using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeadUpDisplay {

	private string spritesPath = "2D/HUD/Gauges/hudGaugeFinale_";

	private Text message;
	private Image spriteMessage;

	/*
	private Image greenGaugeHighlight;
	private Image blueGaugeHighlight;
	private Image yellowGaugeHighlight;
	private Image redGaugeHighlight;
	*/

	private Image greenScoreGauge;
	private Image blueScoreGauge;
	private Image yellowScoreGauge;
	private Image redScoreGauge;

	private Sprite[] greenGaugeSprites;
	private Sprite[] blueGaugeSprites;
	private Sprite[] yellowGaugeSprites;
	private Sprite[] redGaugeSprites;

	private static Image[] greenSideDash;
	private static Image[] blueSideDash;
	private static Image[] yellowSideDash;
	private static Image[] redSideDash;

	///<summary>
	///	[0]: Green, [1]: Blue, [2]: Yellow, [3]: Red
	///</summary>
	public Sprite[] winnerSprite;

	private string portraitSpritesPath = "2D/HUD/Portraits/";
	private Image[] portraitImages;
	private Sprite[] greenPortraitSprites;
	private Sprite[] bluePortraitSprites;
	private Sprite[] yellowPortraitSprites;
	private Sprite[] redPortraitSprites;

	private float gaugeUnit;

	public void Init(){
		message = GameObject.FindGameObjectWithTag("MessageHUD").GetComponent<Text>();
		spriteMessage = GameObject.FindGameObjectWithTag("MessageSprite").GetComponent<Image>();

		winnerSprite = Resources.LoadAll<Sprite>("2D/HUD/Winner");

		gaugeUnit = GameController.victoryScore / 25.0f;

		portraitImages = new Image[4];
		portraitImages[0] = GameObject.FindGameObjectWithTag("GreenPortrait").GetComponent<Image>();
		portraitImages[1] = GameObject.FindGameObjectWithTag("BluePortrait").GetComponent<Image>();
		portraitImages[2] = GameObject.FindGameObjectWithTag("YellowPortrait").GetComponent<Image>();
		portraitImages[3] = GameObject.FindGameObjectWithTag("RedPortrait").GetComponent<Image>();

		greenPortraitSprites = Resources.LoadAll<Sprite>(portraitSpritesPath + "Green");
		bluePortraitSprites = Resources.LoadAll<Sprite>(portraitSpritesPath + "Blue");
		yellowPortraitSprites = Resources.LoadAll<Sprite>(portraitSpritesPath + "Yellow");
		redPortraitSprites = Resources.LoadAll<Sprite>(portraitSpritesPath + "Red");

		greenScoreGauge = GameObject.FindGameObjectWithTag("GreenScoreGauge").GetComponent<Image>();
		blueScoreGauge = GameObject.FindGameObjectWithTag("BlueScoreGauge").GetComponent<Image>();
		yellowScoreGauge = GameObject.FindGameObjectWithTag("YellowScoreGauge").GetComponent<Image>();
		redScoreGauge = GameObject.FindGameObjectWithTag("RedScoreGauge").GetComponent<Image>();

		/*
		greenGaugeHighlight = GameObject.FindGameObjectWithTag("GreenScoreGauge").GetComponentInChildren<Image>();
		blueGaugeHighlight = GameObject.FindGameObjectWithTag("BlueScoreGauge").GetComponentInChildren<Image>();
		yellowGaugeHighlight = GameObject.FindGameObjectWithTag("YellowScoreGauge").GetComponentInChildren<Image>();
		redGaugeHighlight = GameObject.FindGameObjectWithTag("RedScoreGauge").GetComponentInChildren<Image>();
		*/

		greenSideDash = GameObject.Find("GreenSideDash").GetComponentsInChildren<Image>();
		blueSideDash = GameObject.Find("BlueSideDash").GetComponentsInChildren<Image>();
		yellowSideDash = GameObject.Find("YellowSideDash").GetComponentsInChildren<Image>();
		redSideDash = GameObject.Find("RedSideDash").GetComponentsInChildren<Image>();

		greenGaugeSprites = Resources.LoadAll<Sprite>(spritesPath + "GREEN");
		blueGaugeSprites = Resources.LoadAll<Sprite>(spritesPath + "BLUE");
		yellowGaugeSprites = Resources.LoadAll<Sprite>(spritesPath + "YELLOW");
		redGaugeSprites = Resources.LoadAll<Sprite>(spritesPath + "RED");
	}

	public void UpdateGreenGauge(float score){
		greenScoreGauge.sprite = greenGaugeSprites[(int)(score/gaugeUnit)];
	}

	public void UpdateBlueGauge(float score){
		blueScoreGauge.sprite = blueGaugeSprites[(int)(score/gaugeUnit)];
	}

	public void UpdateYellowGauge(float score){
		yellowScoreGauge.sprite = yellowGaugeSprites[(int)(score/gaugeUnit)];
	}

	public void UpdateRedGauge(float score){
		redScoreGauge.sprite = redGaugeSprites[(int)(score/gaugeUnit)];
	}

	public static void UpdateSideDash(int playerIndex, int chargeCount){
		switch(playerIndex){
			case 1: greenSideDash[0].color = chargeCount > 0 ? Color.white : Color.clear;
					greenSideDash[1].color = chargeCount > 1 ? Color.white : Color.clear;
					break;	
			case 2: blueSideDash[0].color = chargeCount > 0 ? Color.white : Color.clear;
					blueSideDash[1].color = chargeCount > 1 ? Color.white : Color.clear;
					break;	
			case 3: yellowSideDash[0].color = chargeCount > 0 ? Color.white : Color.clear;
					yellowSideDash[1].color = chargeCount > 1 ? Color.white : Color.clear;
					break;	
			case 4: redSideDash[0].color = chargeCount > 0 ? Color.white : Color.clear;
					redSideDash[1].color = chargeCount > 1 ? Color.white : Color.clear;
					break;	
		}
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
		Sprite[] tmpSprite = new Sprite[4];
		switch(playerIndex){
			case 1: tmpSprite = greenPortraitSprites; break;
			case 2: tmpSprite = bluePortraitSprites; break;
			case 3: tmpSprite = yellowPortraitSprites; break;
			case 4: tmpSprite = redPortraitSprites; break;
		}
		switch(animalName){
			case "Axolotl": index = 0; break;
			case "Fennec": index = 1; break;
			case "Leopard": index = 2; break;
			case "Loutre": index = 3; break;
		}
		portraitImages[playerIndex-1].sprite = tmpSprite[index];
	}
}
