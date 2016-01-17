using UnityEngine;
using System.Collections;

public class SpawnWave : MonoBehaviour
{
	public GameObject objectToSpawn;
	public GameObject powerUp;
	public float spawnDelay = 1.0f;
	public int numberOfEnemies;

	private int enemyCount;
	private Vector3 lastEnemyPosition;
	private bool powerUpGenerated = false;

	// Use this for initialization
	void Start ()
	{
		enemyCount = numberOfEnemies;
		Spawn();
	}

	void Update()
	{
		if(enemyCount == 0)
		{
			if(transform.childCount <= 0 && !powerUpGenerated)
			{
				powerUpGenerated = true;
				Instantiate(powerUp, lastEnemyPosition, powerUp.transform.rotation);
				Destroy (gameObject);
			}
		}
	}

	void Spawn()
	{
		enemyCount--;
		GameObject instance = Instantiate(objectToSpawn, transform.position, transform.rotation) as GameObject;
		instance.transform.parent = transform;
		if(enemyCount > 0)
		{
			Invoke("Spawn", spawnDelay);
		}
	}

	void Dead(Vector3 position)
	{
		lastEnemyPosition = position;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transform.position, Vector3.one);
	}
}
