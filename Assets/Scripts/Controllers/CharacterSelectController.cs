using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CharacterSelectController : MonoBehaviour {

	public float SpawnDistance;
	private Vector3 LerpVector;
	public float SpeedFactor;

	public struct AnimationState{
		public bool Animating;
		public int Direction;
	}

	private string[] CharacterNames = { "Axolotl", "Leopard", "Fennec", "Loutre" };
	private AnimationState[] AnimationStates = new AnimationState[4];
	private bool[] CanMove = {true, true, true, true};
	private List<Vector3> ReferencePositions = new List<Vector3>();

	public List<GameObject> Characters = new List<GameObject>();
	public Dictionary<string, bool> CharacterAvailability = new Dictionary<string, bool> ();
	public List<GameObject> PlayerSelectedCharacters = new List<GameObject>();
	public List<GameObject> PlayerSwitchingCharacters = new List<GameObject>();
	public string[] FinalSelections = {"null", "null", "null", "null"};
	private List<GamepadDevice> Gamepads = new List<GamepadDevice>();

	public CharacterSelectMenu menu;

	void Awake(){

		DontDestroyOnLoad (transform.gameObject);

		for (int i = 0; i < CharacterNames.Length; i++) {
			CharacterAvailability.Add (CharacterNames [i], true);
		}

		foreach (GameObject character in PlayerSelectedCharacters)
			ReferencePositions.Add (character.transform.position);

		LerpVector = new Vector3 (SpawnDistance, 0, 0);
	}
		
	void SwitchCharacter(int direction, int playerNumber){
		if (!AnimationStates [playerNumber].Animating) {
			bool switched = false;
			int indexOfSelected = -1;
			if(PlayerSelectedCharacters [playerNumber] != null)
				indexOfSelected = Characters.FindIndex (a => a.name == PlayerSelectedCharacters [playerNumber].name);

			while (!switched) {
				indexOfSelected += direction;

				if (indexOfSelected < 0)
					indexOfSelected = 3;
				else if (indexOfSelected > 3)
					indexOfSelected = 0;

				if (CharacterAvailability [Characters [indexOfSelected].name])
					switched = true;
			}

			GameObject newSelectedCharacter = Characters [indexOfSelected];

			Vector3 spawnPosition = new Vector3(-direction*SpawnDistance, PlayerSelectedCharacters [playerNumber].transform.position.y, PlayerSelectedCharacters [playerNumber].transform.position.z);
			Quaternion q = PlayerSelectedCharacters [playerNumber].transform.rotation;
			newSelectedCharacter = Instantiate (newSelectedCharacter, spawnPosition, q) as GameObject;
			newSelectedCharacter.name = Characters [indexOfSelected].name;

			PlayerSwitchingCharacters [playerNumber] = PlayerSelectedCharacters [playerNumber];
			PlayerSelectedCharacters [playerNumber] = newSelectedCharacter;

			AnimationState state;
			state.Animating = true;
			state.Direction = direction;
			CanMove [playerNumber] = false;
			AnimationStates [playerNumber] = state;
		}
	}
		
	void Update () {
		Gamepads = GamepadInput.Instance.gamepads;

		for (int i = 0; i < Gamepads.Count; i++) {
			HandlePlayerInput (i);
		}

		for (int i = 0; i < AnimationStates.Length; i++) {
			if (AnimationStates [i].Animating)
				Animate (i, AnimationStates[i].Direction);
		}

		if(menu != null)
			menu.pressStartImg.enabled = AllPlayersReady ();

		if(Input.GetKeyDown(KeyCode.A))
			Select (0);
		if(Input.GetKeyDown(KeyCode.Z))
			Select (1);
		if(Input.GetKeyDown(KeyCode.E))
			Select (2);
		if(Input.GetKeyDown(KeyCode.R))
			Select (3);

		/*
		if (Input.GetKeyUp(KeyCode.Space)) {
			SceneManager.LoadScene ("LD_Forest");
		}
		*/
	}

	void HandlePlayerInput (int playerNumber)
	{
		if (CanMove [playerNumber]) {
			if (Gamepads [playerNumber].GetAxis (GamepadAxis.LeftStickX) < -0.5)
				SwitchCharacter (-1, playerNumber);
			else if (Gamepads [playerNumber].GetAxis (GamepadAxis.LeftStickX) > 0.5)
				SwitchCharacter (1, playerNumber);
			if (Gamepads [playerNumber].GetButtonUp (GamepadButton.Action1))
				Select (playerNumber);
		}

		if (Gamepads[playerNumber].GetButtonUp (GamepadButton.Action2) && FinalSelections[playerNumber] != "null")
			Cancel (playerNumber);

		if(Gamepads [playerNumber].GetButtonUp(GamepadButton.Start) && AllPlayersReady())
			SceneManager.LoadScene ("LD_Forest");
	}

	void Select(int playerNumber){
		PlayerSelectedCharacters [playerNumber].GetComponentInChildren<Animator>().SetBool("BackFlip", true);
		menu.UpdatePlayerStateSprite(playerNumber, 1);
		FinalSelections[playerNumber] = PlayerSelectedCharacters [playerNumber].name;
		CharacterAvailability [PlayerSelectedCharacters [playerNumber].name] = false;
		KeepFromSelecting (playerNumber);
		CanMove [playerNumber] = false;
	}

	void KeepFromSelecting(int playerNumber){
		for (int i = 0; i < 4; i++) {
			if (i != playerNumber && PlayerSelectedCharacters [playerNumber].name == PlayerSelectedCharacters [i].name  )
				SwitchCharacter (1, i);
		}
	}

	void Cancel(int playerNumber){
		menu.UpdatePlayerStateSprite(playerNumber, 0);
		FinalSelections[playerNumber] = "null";
		CharacterAvailability [PlayerSelectedCharacters [playerNumber].name] = true;
		CanMove [playerNumber] = true;
	}

	bool AllPlayersReady(){
		bool ready = true;
		foreach (string characterName in FinalSelections) {
			ready = ready && (characterName != "null");
		}
		return ready;
	}

	void Animate(int playerNumber, int direction){
		Vector3 SelectedPosition = PlayerSelectedCharacters [playerNumber].transform.position;
		Vector3 SwitchingPosition = PlayerSwitchingCharacters [playerNumber].transform.position;

		PlayerSelectedCharacters [playerNumber].transform.position = Vector3.Lerp (SelectedPosition, ReferencePositions[playerNumber], Time.deltaTime * SpeedFactor);
		PlayerSwitchingCharacters [playerNumber].transform.position = Vector3.Lerp (SwitchingPosition, ReferencePositions[playerNumber] + direction*LerpVector, Time.deltaTime * SpeedFactor);

		if (SelectedPosition.x > -4 && SelectedPosition.x < 0) {
			AnimationStates [playerNumber].Animating = false;
			CanMove [playerNumber] = true;
			Destroy (PlayerSwitchingCharacters [playerNumber]);
		}
	}

	void OnLevelWasLoaded(int loadedLevel){
		if(loadedLevel == 1){
			GameObject winner = GameObject.Find("Winner");
			Destroy(winner);
		}
	}
}
