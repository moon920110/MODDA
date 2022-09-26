using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverySpawner : MonoBehaviour
{
    #region Variables
    public string spawnPointTag = "RecoverySpawners";
	public bool alwaysSpawn = true;
	public GameObject firstaidPrefab;//= Resources.Load("FirstAid");
	public int xPos;
	public int zPos;
	public int firstaidCount;	
	public GameObject[] specificObject;
	public GameObject prefabObject;
	
	public bool shouldSpawn = true;
	public float loopTime = 1f;
	#endregion
	void Start()
	{
		prefabObject= (GameObject)Resources.Load("FirstAid");		
	}

		
    void Update()
    {
		// Spawns Recovery Kits on pre-determined locations
		if (Input.GetKeyDown("g")) 
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

		// Spawns Recovery Kits on random locations
		if (Input.GetKeyDown("f")) 
		{
			StartCoroutine(randomRecoverySpawn());
		}
	}

    

	IEnumerator randomRecoverySpawn() // Spawns Recovery Kits on random locations
	{
        while (firstaidCount <= 25)
        {
			xPos = Random.Range(-40, 40);
			zPos = Random.Range(-40, 40);
			Instantiate(Resources.Load("FirstAid"), new Vector3(xPos, 0, zPos), Quaternion.identity);
			
			yield return new WaitForSeconds(0.1f);
			firstaidCount += 1;
		}
    }
}



