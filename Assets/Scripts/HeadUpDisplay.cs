using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeadUpDisplay {

	private string spritesPath = "2D/HUD/Gauges/hudGaugeFinale_";

	private Text message;

	private Image greenScoreGauge;
	private Image blueScoreGauge;
	private Image yellowScoreGauge;
	private Image redScoreGauge;

	private Sprite[] greenGaugeSprites;
	private Sprite[] blueGaugeSprites;
	private Sprite[] yellowGaugeSprites;
	private Sprite[] redGaugeSprites;

	private float gaugeUnit;

	public void Init(){
		message = GameObject.FindGameObjectWithTag("MessageHUD").GetComponent<Text>();

		gaugeUnit = GameController.victoryScore / 25.0f;

		greenScoreGauge = GameObject.FindGameObjectWithTag("GreenScoreGauge").GetComponent<Image>();
		blueScoreGauge = GameObject.FindGameObjectWithTag("BlueScoreGauge").GetComponent<Image>();
		yellowScoreGauge = GameObject.FindGameObjectWithTag("YellowScoreGauge").GetComponent<Image>();
		redScoreGauge = GameObject.FindGameObjectWithTag("RedScoreGauge").GetComponent<Image>();
	
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
}
