using UnityEngine;
using System.Collections;

public class FadeInOut : MonoBehaviour {


	public Texture2D fadeTexture;
	private float fadeSpeed = 0.2f;
	private int drawDepth = -1000;

	private float alpha = 1.0f; 
	private int fadeDir = -1;

	void OnGUI(){

		alpha += fadeDir * fadeSpeed * Time.deltaTime;  
		alpha = Mathf.Clamp01(alpha);   

		Color color = GUI.color;
		color.a = alpha;
		GUI.color = color;

		GUI.depth = drawDepth;

		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
	}
}
