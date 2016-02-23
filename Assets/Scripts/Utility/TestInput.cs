using UnityEngine;
using System.Collections;

public class TestInput : MonoBehaviour {
	
	//GamepadDevice gpDev;
	[SerializeField] public float[] ShakeCameraArgs = new float[3];

	void Start() {
		//gpDev = GamepadInput.Instance.AssignGamepad(0);
	}

	void Update () {
		/*
		if(Time.time - gpDev.lastInputTime > 10.0f){
			gpDev = GamepadInput.Instance.AssignGamepad(0);
		}
		//if(gpDev.GetButtonDown(GamepadButton.Action1)) Debug.Log("Button A Pressed");
		*/
		if(Input.GetKeyDown(KeyCode.A)){
			//StartCoroutine(CameraController.Instance.Shake(ShakeCameraArgs[0], ShakeCameraArgs[1], ShakeCameraArgs[2]));
			StartCoroutine(testVibration());
		}
	}

	private IEnumerator testVibration(){
		GamepadInput.Instance.gamepads[0].StartVibration(0.5f, 0.5f);
		yield return new WaitForSeconds(0.8f);
		GamepadInput.Instance.gamepads[0].StopVibration();
	}
}