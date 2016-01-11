using UnityEngine;
using System.Collections;

public class Massivity {

	public static string effectTag = "Massivity";
	private float duration = 5.0f;
	public Vector3 initScale {get; set;}
	private float scaleFactor = 0.0f;
	private float speed = 5.0f;
	
	public bool runEffect(GameObject targetPlayer, float timestamp){
		if(Time.time - timestamp <= duration){
			scaleFactor = Mathf.Clamp01(scaleFactor + speed * Time.deltaTime);
			targetPlayer.transform.localScale = Vector3.Slerp(initScale, initScale*2.0f, scaleFactor);
			targetPlayer.GetComponent<Player>().immune = true;
			return true;
		}
		else{
			targetPlayer.transform.localScale = initScale;
			targetPlayer.GetComponent<Player>().immune = false;
			return false;
		}
	}
}