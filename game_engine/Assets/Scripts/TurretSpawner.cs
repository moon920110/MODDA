using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DDA
{
	public class TurretSpawner : MonoBehaviour
	{
		#region Variables
		public GameObject TurretAI;
		public string EnemySpawnPointTag = "EnemySpawners";
		public bool alwaysSpawn = true;
		public int xPos;
		public int zPos;
		public int EnemyCount;
		public GameObject prefabEnemyObject;

		#endregion
		void Start()
		{
			prefabEnemyObject = (GameObject)Resources.Load("TurretAI");
		}
		void Update()
		{
			// Spawns Enemies on pre-determined locations
			prefabEnemyObject = (GameObject)Resources.Load("TurretAI");
			if (Input.GetKeyDown("h"))
			{

				GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(EnemySpawnPointTag);

				foreach (GameObject spawnPoint in spawnPoints)
				{
					//int randomPrefab = Random.Range(0, prefabsToSpawn.Count); // When we have several kinds of enemies
					if (alwaysSpawn)
					{
						GameObject pts = Instantiate(prefabEnemyObject);
						pts.transform.position = spawnPoint.transform.position;
					}
					else
					{
						int spawnOrNot = Random.Range(0, 2);
						if (spawnOrNot == 0)
						{
							GameObject pts = Instantiate(prefabEnemyObject);
							pts.transform.position = spawnPoint.transform.position;
						}
					}
				}


			}

			// Spawns Enemies on random locations
			if (Input.GetKeyDown("j"))
			{
				StartCoroutine(randomEnemySpawn());
			}
		}
		IEnumerator randomEnemySpawn() // Spawns Enemies on random locations
		{
			//EnemyCount = GameObject.FindGameObjectsWithTag("Turret").Length;
			while (EnemyCount <= 15)
			{
				xPos = Random.Range(-40, 40);
				zPos = Random.Range(-40, 40);
				Instantiate(Resources.Load("TurretAI"), new Vector3(xPos, 0, zPos), Quaternion.identity);

				yield return new WaitForSeconds(0.1f);
				EnemyCount += 1;
			}

		}
	}

}