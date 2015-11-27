using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickUpSpawn : MonoBehaviour {

	private List<Vector3> spawnPositions = new List<Vector3>();
	public GameObject pickUpPrefab;

	GameObject previousPickUp;
	int previousIndex = -1;

	void Awake(){
		spawnPositions.Add(new Vector3 (21.2f, 1.5f, 9.5f));
		spawnPositions.Add(new Vector3 (-8.85f, 1.5f, 9.5f));
		spawnPositions.Add(new Vector3 (3.35f, 1.5f, 9.5f));
		spawnPositions.Add(new Vector3 (16.38f, 1.5f, 9.5f));
		spawnPositions.Add(new Vector3 (-21.23f, 1.5f, 0.69f));
		spawnPositions.Add(new Vector3 (-9.23f, 1.5f, 0.69f));
		spawnPositions.Add(new Vector3 (4.04f, 1.5f, 0.69f));
		spawnPositions.Add(new Vector3 (15.79f, -9.4f, 17f));
		spawnPositions.Add(new Vector3 (-20.89f, 1.5f, -8.27f));
		spawnPositions.Add(new Vector3 (-9.31f, 1.5f, -8.27f));
		spawnPositions.Add(new Vector3 (3.87f, 1.5f, -8.27f));
		spawnPositions.Add(new Vector3 (15.87f, 1.5f, -8.27f));
		spawnPositions.Add(new Vector3 (-21.23f, 1.5f, -17.06f));
		spawnPositions.Add(new Vector3 (-8.81f, 1.5f, -17.06f));
		spawnPositions.Add(new Vector3 (3.78f, 1.5f, -17.06f));
		spawnPositions.Add(new Vector3 (16.03f, 1.5f, -17.06f));
	}

	// Use this for initialization
	void Start () {
		InvokeRepeating("randomSpawn", 2, 5F);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void randomSpawn(){
		Destroy (previousPickUp);
		int index = previousIndex;
		while(index == previousIndex)
			index = Random.Range (0, spawnPositions.Count - 1);
		previousPickUp = Instantiate (pickUpPrefab, spawnPositions [index], Quaternion.identity) as GameObject;
		previousIndex = index;
	}
}
