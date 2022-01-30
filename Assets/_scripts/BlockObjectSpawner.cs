using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockObjectSpawner : MonoBehaviour
{

    public List<GameObject> prefabToSpawn;
    public List<GameObject> spawnPoints;
    
    void Awake()
    {
        spawnPoints= new List<GameObject>();
        foreach (Transform tr in transform)
        {
            spawnPoints.Add(tr.gameObject);
        }
        SpawnRandom();    
    }

    void SpawnRandom(){
        int randomIndexPrefab= UnityEngine.Random.Range(0,prefabToSpawn.Count);
        int randomIndexSpawnPoint= UnityEngine.Random.Range(0,spawnPoints.Count);

        bool canSpawn= UnityEngine.Random.Range(0,100)>50;
        if (canSpawn){
            GameObject currentSpawned= Instantiate(
                prefabToSpawn[randomIndexPrefab],
                spawnPoints[randomIndexSpawnPoint].transform
            );
            currentSpawned.transform.position=currentSpawned.transform.position+Vector3.up*.5f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
