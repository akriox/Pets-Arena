using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
	
	//GamepadDevice gpDev;

	void Start() {
		//gpDev = GamepadInput.Instance.AssignGamepad(0);
	}

	void Update () {
		/*
		if(Time.time - gpDev.lastInputTime > 10.0f){
			gpDev = GamepadInput.Instance.AssignGamepad(0);
		}
		*/

		//if(gpDev.GetButtonDown(GamepadButton.Action1)) Debug.Log("Button A Pressed");

		//if(GamepadInput.Instance.gamepads[0].GetButtonDown(GamepadButton.Action1))  Debug.Log("Button A Pressed");
	}
}
