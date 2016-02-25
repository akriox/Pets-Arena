using UnityEngine;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour {

	struct playerPointData {
		public Vector2 position;
		public float weight;
	};

	//attributs utilisés pour récupérer la position courante 
	//de chaque joueur lors du calcul du barycentre de spawn
	private GameObject _greenPlayer;
	private GameObject _bluePlayer;
	private GameObject _yellowPlayer;
	private GameObject _redPlayer;

	//attribut utilisé pour récupérer les scores des joueurs
	//dont seront déduits les poids des points du barycentre
	GameController _gameController;
	EventController _eventController;

	public float CurrentTimer = 0;
	public float SpawnInterval = 1.0f;

	//nombre de faces sur le dé
	public int DiceFacesNumber = 6;
	//nombre de faces de type Event sur le dé
	public int EventFacesNumber = 4;
	//nombre de faces de type Weapon sur le dé
	public int WeaponFacesNumber = 2;

	//faces du dés, « e » pour Event, « w » pour Weapon
	string[] DiceFaces;

	GameObject previousPickUp;
	public GameObject pickUpPrefab;
	GameObject previousEvent;
	//prefabs de chaque Event
	public List<UnityEngine.Object> EventPrefabs = new List<UnityEngine.Object>();
	//positions de spawn possible pour chaque Event
	List<Vector2> EventPositions = new List<Vector2>();

	//hauteur de spawn des objets
	private float SpawnHeight = 8.0f;

    //pool commun d'armes
    private string[] pool;
    private int poolIndex;

    void Awake(){
		_gameController = gameObject.GetComponent<GameController> ();
		_eventController = gameObject.GetComponent<EventController> ();

		EventPositions.Add(new Vector2 (5f, 9.5f));
		EventPositions.Add(new Vector2 (-8.85f, 9.5f));
		EventPositions.Add(new Vector2 (3.35f, 9.5f));
		EventPositions.Add(new Vector2 (8f, 9.5f));
		EventPositions.Add(new Vector2 (-10f, 0.69f));
		EventPositions.Add(new Vector2 (-9.23f, 0.69f));
		EventPositions.Add(new Vector2 (4.04f, 0.69f));
		EventPositions.Add(new Vector2 (8f, 17f));
		EventPositions.Add(new Vector2 (-10f, -8.27f));
		EventPositions.Add(new Vector2 (-9.31f, -8.27f));
		EventPositions.Add(new Vector2 (3.87f, -8.27f));
		EventPositions.Add(new Vector2 (15.87f, -8.27f));
		EventPositions.Add(new Vector2 (-10f, -17.06f));
		EventPositions.Add(new Vector2 (-5f, -17.06f));
		EventPositions.Add(new Vector2 (3.78f, -17.06f));
		EventPositions.Add(new Vector2 (5f, -17.06f));
	}

	// Use this for initialization
	void Start () {
		_greenPlayer = GameObject.FindGameObjectWithTag ("P1");
		_bluePlayer = GameObject.FindGameObjectWithTag ("P2");
		_yellowPlayer = GameObject.FindGameObjectWithTag ("P3");
		_redPlayer = GameObject.FindGameObjectWithTag ("P4");

		DiceFaces = new string[DiceFacesNumber];
		FillDice ();

        poolIndex = 0;
        pool = new string[PowerUp.count];
        pool[0] = AttractBall.effectTag;
        pool[1] = RepulsiveWave.effectTag;
        pool[2] = GlobalStun.effectTag;
        pool[3] = Massivity.effectTag;
        shufflePool();
    }
	
	void Update(){
		CurrentTimer += Time.deltaTime;
		if (CurrentTimer >= SpawnInterval) {
			SpawnItem();
			CurrentTimer = 0;
		}

        if (poolIndex > PowerUp.count-1){
            shufflePool();
            poolIndex = 0;
        }
    }

	void FillDice(){
		for (int i = 0; i<EventFacesNumber; i++)
			DiceFaces[i] = "e";
		for (int j = EventFacesNumber-1; j<(EventFacesNumber+WeaponFacesNumber); j++)
			DiceFaces[j] = "w";
	}

	string RollDice(){
		return DiceFaces[UnityEngine.Random.Range(0, DiceFacesNumber-1)];
	}

	List<playerPointData> GetBarycenterData(){
		List<playerPointData> barycenterPoints = new List<playerPointData>();

		playerPointData redData;
		redData.position = new Vector2(_redPlayer.transform.position.x, _redPlayer.transform.position.z);
		if (_gameController.redScore == 0)
			redData.weight = 1;
		else if (_gameController.redScore < 0)
			redData.weight = -_gameController.redScore;
		else
			redData.weight = 1/_gameController.redScore;
		
		barycenterPoints.Add (redData);

		playerPointData greenData;
		greenData.position = new Vector2(_greenPlayer.transform.position.x, _greenPlayer.transform.position.z);
		if (_gameController.greenScore == 0)
			greenData.weight = 1;
		else if (_gameController.greenScore < 0)
			greenData.weight = -_gameController.greenScore;
		else
			greenData.weight = 1/_gameController.greenScore;

		barycenterPoints.Add (greenData);

		playerPointData blueData;
		blueData.position = new Vector2(_bluePlayer.transform.position.x, _bluePlayer.transform.position.z);
		if (_gameController.blueScore == 0)
			blueData.weight = 1;
		else if (_gameController.blueScore < 0)
			blueData.weight = -_gameController.blueScore;
		else
			blueData.weight = 1/_gameController.blueScore;

		barycenterPoints.Add (blueData);

		playerPointData yellowData;
		yellowData.position = new Vector2(_yellowPlayer.transform.position.x, _yellowPlayer.transform.position.z);
		if (_gameController.yellowScore == 0)
			yellowData.weight = 1;
		else if (_gameController.yellowScore < 0)
			yellowData.weight = -_gameController.yellowScore;
		else
			yellowData.weight = 1/_gameController.yellowScore;
		barycenterPoints.Add (yellowData);

		//on classe la liste de joueurs par poids en ordre descendant
		//et on supprime le dernier, le plus petit et donc le joueur le plus en avance
		barycenterPoints.Sort ((s1, s2) => -1 * s1.weight.CompareTo (s2.weight));
		barycenterPoints.RemoveAt (barycenterPoints.Count - 1);

		return barycenterPoints;
	}

	Vector2 ComputeBarycenterCoordinates(){
		List<playerPointData> points = GetBarycenterData ();

		float X_numerator = 0;
		float Y_numerator = 0;
		foreach (playerPointData ppd in points) {
			X_numerator += ppd.weight*ppd.position.x;
			Y_numerator += ppd.weight*ppd.position.y;
		}

		float denominator = 0;

		foreach (playerPointData ppd in points) {
			denominator += ppd.weight;
		}

		return new Vector2 (X_numerator/denominator, Y_numerator/denominator);
	}

	void SpawnItem(){
		if (RollDice () == "e" && !_eventController.activated) {
			Destroy(previousEvent);
			Vector2 randomEventPosition = EventPositions[UnityEngine.Random.Range(0, EventPositions.Count-1)];
			Vector3 spawnPos = new Vector3(randomEventPosition.x, SpawnHeight, randomEventPosition.y);
			UnityEngine.Object randomPrefab = EventPrefabs[UnityEngine.Random.Range(0, EventPrefabs.Count)];
			previousEvent = Instantiate (randomPrefab, spawnPos, Quaternion.identity) as GameObject;

		}
		else {
			Destroy(previousPickUp);
			Vector2 barycenter = ComputeBarycenterCoordinates();
			Vector3 spawnPos = new Vector3(barycenter.x, SpawnHeight, barycenter.y);
			previousPickUp = Instantiate (pickUpPrefab, spawnPos, Quaternion.identity) as GameObject;
            previousPickUp.GetComponent<PickUp>().effectTag = pool[poolIndex];
            poolIndex++;
        }	
	}

    private void shufflePool()
    {
        string tmp;
        int i, j;

        for (i = PowerUp.count - 1; i > 0; i--)
        {
            j = Random.Range(0, i);
            tmp = pool[i];
            pool[i] = pool[j];
            pool[j] = tmp;
        }
    }
}
