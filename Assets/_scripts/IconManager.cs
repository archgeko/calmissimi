using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NaughtyAttributes;

public class IconManager : MonoBehaviour
{
    public List<GameObject> UIPrefabs;
    public List<GameObject> UISpawned;
    public GameObject spawnPoint;
    public float distanceFactorFromLatestSpawn;
    public int numberToSpawn;

    // Start is called before the first frame update
    private void Awake()
    {
        UISpawned = new List<GameObject>();
    }

    [Button]
    public void SpawnUI()
    {
        GameObject currSpawnedUI;
        GameObject prefabToSpawn=UIPrefabs[0];// da rendere random
        if (UISpawned.Count == 0)
        {
            currSpawnedUI = Instantiate(prefabToSpawn, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
        else
        {   
            float maxValue = UISpawned.Max(x => x.transform.position.y);
            Transform highestIcon = UISpawned.First(x => x.transform.position.y == maxValue).transform;
            currSpawnedUI = Instantiate(prefabToSpawn, highestIcon.transform.position+ Vector3.up*distanceFactorFromLatestSpawn, highestIcon.transform.rotation);
        }
        currSpawnedUI.transform.parent=this.transform;
        UISpawned.Add(currSpawnedUI);
    }

}
