using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
	//NOT WRITTEN YET
	/*public string spawnPointTag = "WeaponSpawners";
	public bool alwaysSpawn = true;
	

	public GameObject riflePrefab;//= Resources.Load("Rifle");
	public int xPos;
	public int zPos;
	public int rifleCount;
	//public int rifleMax = 50;
	public GameObject[] specificObject;
	public GameObject prefabObject;
	//public GameObject riflePrefab;
	public bool shouldSpawn = true;
	public float loopTime = 1f;


	//public bool stopSpawning = false;
	//public float spawnTime=0.1f;
	//public float spawnDelay=1f;
	void Start()
	{
		prefabObject = (GameObject)Resources.Load("Rifle");
	}

	void Update()
	{
		// Spawns Weapon on pre-determined locations
		if (Input.GetKeyDown("o"))
		{
			GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(spawnPointTag);
			foreach (GameObject spawnPoint in spawnPoints)
			{
				//int randomPrefab = Random.Range(0, prefabsToSpawn.Count); 
				if (alwaysSpawn)
				{
					GameObject pts = Instantiate(prefabObject);
					pts.transform.position = spawnPoint.transform.position;
				}
				else
				{
					int spawnOrNot = Random.Range(0, 2);
					if (spawnOrNot == 0)
					{
						GameObject pts = Instantiate(prefabObject);
						pts.transform.position = spawnPoint.transform.position;
					}
				}
			}
		}

		// Spawns Weapon on random locations
		if (Input.GetKeyDown("p"))
		{
			StartCoroutine(randomWeaponSpawn());
		}
	}



	IEnumerator randomWeaponSpawn() // Spawns Weapon on random locations
	{
		while (rifleCount <= 25)
		{
			xPos = Random.Range(-40, 40);
			zPos = Random.Range(-40, 40);
			Instantiate(Resources.Load("Rifle"), new Vector3(xPos, 0, zPos), Quaternion.identity);

			yield return new WaitForSeconds(0.1f);
			rifleCount += 1;
		}
	}*/
}
