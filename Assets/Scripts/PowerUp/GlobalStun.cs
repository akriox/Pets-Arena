using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalStun {

	public static string effectTag = "GlobalStun";
	private List<Player> targets = new List<Player>();

	public void init(GameObject caster){
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		int i;
		for(i = 0; i < players.Length; i++){
			if(players[i].name != caster.name){
				targets.Add(players[i].GetComponent<Player>());
			}
		}
	}

	public bool runEffect(){
		int i;
		for(i = 0; i < targets.Count; i++){	
			targets[i].paralyzed = true;
		}
		return false;
	}
}
