using UnityEngine;
using System.Collections.Generic;

public class FlappyObstaclesSpawner : MonoBehaviour {

	public GameObject player;
	public GameObject obstaclePrefab;
	public float firstObstacteX = 20; 
	public float distanceBetweenObstacles = 10;
	List<GameObject> spawnedObstacles = new List<GameObject>();
	List<float> spawnedObstaclesX = new List<float>();
	FlappyPlayer fp;

	void Start (){
		fp = player.GetComponent<FlappyPlayer> ();
		for (int i = 0; i < 5; i++) {
			Spawn ( firstObstacteX + i * distanceBetweenObstacles );
		}
	}

	void Update (){
		for (int i = 0; i < spawnedObstaclesX.Count - 1; i++) {
			if (player.transform.position.x > spawnedObstaclesX[i]) {
				fp.IncreaseScore ();
				spawnedObstaclesX.RemoveAt (i);
				break;
			}
		}
		for (int i = 0; i < spawnedObstacles.Count - 1; i++) {
			GameObject obs = spawnedObstacles [i];
			if (player.transform.position.x > obs.transform.position.x + distanceBetweenObstacles * 2){
				RemoveObstacle ( obs );
				CreateNewObstacle ();
			}
		}
	}

	private void Spawn( float x ) {
		GameObject spawned = GameObject.Instantiate( obstaclePrefab );
        spawned.transform.parent = transform;
		float y = Random.Range ( -3.5f, 3.5f );
		spawned.transform.position = new Vector3 ( x, y, 0 );
		spawned.transform.localScale = new Vector3 (1, Random.Range (0.75f, 1.15f), 1);
		spawnedObstacles.Add ( spawned );
		spawnedObstaclesX.Add ( x );
    }

	private void RemoveObstacle (GameObject obj){
		if (obj != null) {
			spawnedObstacles.Remove (obj);
			Destroy (obj);
		}
	}

	private void CreateNewObstacle (){
		GameObject lastObstacle = spawnedObstacles [spawnedObstacles.Count - 1];
		Spawn (lastObstacle.transform.position.x + distanceBetweenObstacles );
	}

}