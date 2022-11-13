using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
	[Header("Road Settings")]

	[SerializeField] private int _maxRoadsOnScreen;             // The maximum number of roads that can be active at one time
	[SerializeField] private float _newSpawnDistance;                       // The point in which a new road will spawn
	[SerializeField] private float _maxDistance;                   // The maximum distance that parent road can move before being reset
	[SerializeField] private bool _generateWithObstacles;                               

	[Header("Road Objects")]
	[SerializeField] private GameObject[] _roads;
	[SerializeField] private GameObject _roadParent;			

	private List<GameObject> _activeRoads;
	private float _spawnZ;                                          // The Z position for where the next road will spawn
	private GameManager _gameManager;

	public float RoadSize;
	[HideInInspector] public bool GenerateWithObstacles => _generateWithObstacles;

	private void Awake()
	{
		// Creating a new list of currently active roads (the roads that are in the scene at the moment)
		_activeRoads = new List<GameObject>();
		_gameManager = FindObjectOfType<GameManager>();
		// Initilizing the game, creating the first set of roads. Can be changed with maxRoadsOnScreen)

		_generateWithObstacles = false;

		for (int i = 0; i < _maxRoadsOnScreen; i++)
		{
			if (i > _gameManager.WarmupStageLength)
			{
				_generateWithObstacles = true;
			}
			GenerateRoad(Random.Range(0, _roads.Length));
		}
	}

	private void Update()
	{
		// Checking if the road has reached it's final destination
		if (_activeRoads[0].transform.position.z < _newSpawnDistance * -1)
		{
			// Removing the road from the active list and generating a new road at the end
			GameObject roadToDestroy = _activeRoads[0];
			_activeRoads.Remove(_activeRoads[0]);
			Destroy(roadToDestroy);
			GenerateRoad(Random.Range(0,_roads.Length));
		}

		// Checking if the road parent has reached the maximum distance it can go
		if (_roadParent.transform.position.z < (_maxDistance * -1))
		{
			// ResetRoad(); 
		}
	}
	
	void GenerateRoad(int roadIndex)
	{
		// Creating a new road prefab
		GameObject currentRoad;
		currentRoad = Instantiate(_roads[roadIndex]);

		// Setting the parent to the road parent object in the scene, moving the new road to end of the line, and adding it to the list of active roads
		currentRoad.transform.SetParent(_roadParent.transform);
		currentRoad.transform.localPosition = new Vector3(0, 0, _spawnZ);
		_activeRoads.Add(currentRoad);

		// Incrementing the next spawn Z by the length of the current road
		float currentRoadSize = currentRoad.GetComponent<Road>().GetRoadSize();
		RoadSize = currentRoadSize;
		_spawnZ += currentRoadSize;

	}

	void ResetRoad()
	{
		// Resetting the road parent's transform to the origin (0,0,0)
		_roadParent.transform.position = new Vector3();
		_spawnZ = 0;

		// Looping through all the active roads and setting them at the origin + spawnZ
		for (int i = 0; i < _activeRoads.Count; i++)
		{
			_activeRoads[i].transform.localPosition = new Vector3(0, 0, _spawnZ);

			// Incrementing spawnZ by the current road's length
			float activeRoadSize = _activeRoads[i].GetComponent<Road>().GetRoadSize();
			_spawnZ += activeRoadSize;
		}
	}

}
