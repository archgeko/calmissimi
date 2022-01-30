using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NaughtyAttributes;
using System;

public class IconManager : MonoBehaviour
{
    public List<GameObject> UIPrefabs;
    public List<GameObject> UISpawned;
    public GameObject spawnPoint;
    public float distanceFactorFromLatestSpawn;
    public int numberToSpawn;
    private bool toDestroy=false;

    // Start is called before the first frame update
    private void Awake()
    {
        UISpawned = new List<GameObject>();
        UIIconController.AnyUIControllerGrabbed+=FreezeAllIcons;
        UIIconController.AnyUIControllerDropped+=UnfreezeAllIcons;
    }

    void Start()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            SpawnUI();
        }
    }

    

    [Button]
    public void SpawnUI()
    {
        GameObject currSpawnedUI;
        int indexToSpawn=UnityEngine.Random.Range(0,UIPrefabs.Count);
        GameObject prefabToSpawn=UIPrefabs[indexToSpawn];// da rendere random
        if (UISpawned.Count == 0)
        {
            currSpawnedUI = Instantiate(prefabToSpawn, spawnPoint.transform.position, spawnPoint.transform.rotation);
            currSpawnedUI.transform.name=$"Elemento {UISpawned.Count}";
        }
        else
        {   
            float maxValue = UISpawned.Max(x => x.transform.position.y);
            Transform highestIcon = UISpawned.First(x => x.transform.position.y == maxValue).transform;
            currSpawnedUI = Instantiate(prefabToSpawn, highestIcon.transform.position+ Vector3.up*highestIcon.transform.position.y*.20f, highestIcon.transform.rotation);
            currSpawnedUI.transform.name=$"Elemento {UISpawned.Count}";
        }
        currSpawnedUI.transform.parent=this.transform;
        currSpawnedUI.transform.localScale=Vector3.one;
        UISpawned.Add(currSpawnedUI);
    }

    public void LevelUp(GameObject dropper)
    {
        Debug.Log("POTENZIAMENTOS");
        dropper.GetComponent<UIIconController>().ResetPosition();
        UISpawned.Remove(dropper);
        Destroy(dropper);
    }

    public void DroppedOnPanel(GameObject dropper)
    {
        Debug.Log("Azione fatta");
        dropper.GetComponent<UIIconController>().ResetPosition();
        UISpawned.Remove(dropper);
        Destroy(dropper);
    }

    public void FreezeAllIcons(){
        UISpawned.ForEach(
            x=>{
                x.GetComponent<UIIconController>().Freeze();
            }
        );
    }
    public void UnfreezeAllIcons(){
        UISpawned.ForEach(
            x=>{
                x.GetComponent<UIIconController>().Unfreeze();
            }
        );
    }

}
