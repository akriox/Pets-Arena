using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalStun {

	public static string effectTag = "GlobalStun";
	private List<Player> targets = new List<Player>();
	private GameObject[] players = new GameObject[4];

	public void init(GameObject caster){
		int i;
		for(i = 0; i < 4; i++){
			string tag = "P" + (i+1);
			players[i] = GameObject.FindGameObjectWithTag(tag);
		}
		for(i = 0; i < players.Length; i++){
			if(players[i].tag != caster.tag){
				targets.Add(players[i].GetComponent<Player>());
			}
		}
	}

	public bool runEffect(){
		int i;
		for(i = 0; i < targets.Count; i++){	
			targets[i].paralyzed = true;
			targets[i].anim.SetTrigger("Stun");
		}
		return false;
	}
}
