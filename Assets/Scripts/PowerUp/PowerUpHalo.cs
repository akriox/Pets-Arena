using UnityEngine;
using System.Collections;

public class PowerUpHalo : MonoBehaviour {

	private ParticleSystem[] particles;

	private Color[] assignedColor;

	public static Color[] greenPlayer = { new Color(156f/255f, 1f, 143f/255f), new Color(58f/255f, 1f, 31f/255f), new Color(137f/255f, 156f/255f, 121f/255f, 21f/255f) };
	public static Color[] bluePlayer = { new Color(139f/255f, 226f/255f, 1f), new Color(5f/255f, 193f/255f, 1f), new Color(121f/255f, 147f/255f, 156f/255f, 21f/255f) };
	public static Color[] yellowPlayer = { new Color(1f, 243f/255f, 150f/255f), new Color(234f/255f, 208f/255f, 0f), new Color(71f/255f, 71f/255f, 11f/255f, 21f/255f)} ;
	public static Color[] redPlayer = { new Color(1f, 171f/255f, 171f/255f), new Color(1f, 5f/255f, 5f/255f), new Color(156f/255f, 156f/255f, 121f/255f, 21f/255f) };

	void Awake () {
		particles = GetComponentsInChildren<ParticleSystem>();
	}

	public void setColor(int playerIndex){
		switch(playerIndex){
			case 1: assignedColor = greenPlayer; break;
			case 2: assignedColor = bluePlayer; break;
			case 3: assignedColor = yellowPlayer; break;
			case 4: assignedColor = redPlayer; break;
		}
		for(int i = 0; i < assignedColor.Length; i++){
			particles[i].startColor = assignedColor[i];
		}
	}
}
