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
    public static Action<ActionTypes, GameObject> PanelActionActivated;
    
    private bool toDestroy=false;

    // Start is called before the first frame update
    private void Awake()
    {
        UISpawned = new List<GameObject>();
        UIIconController.AnyUIControllerGrabbed+=FreezeAllIcons;
        UIIconController.AnyUIControllerDropped+=UnfreezeAllIcons;
        Dropzone.DicePanelEvent+=OnPanelEvent;
        Dropzone.DiceLevelupEvent+=OnLevelupEvent;
        Dropzone.DiceLevelupFailEvent+=OnLevelupFailedEvent;
    }

    private void OnLevelupFailedEvent(GameObject dropzone, UIIconController dropper)
    {
        dropper.ResetPosition();
    }

    private void OnLevelupEvent(GameObject dropzone, UIIconController dropper)
    {
        UnfreezeAllIcons(dropper.gameObject);
        LevelUp(dropper.gameObject);
        SpawnUI();
    }

    private void OnPanelEvent(GameObject dropzone, UIIconController dropper)
    {
        PanelActionActivated?.Invoke(dropper.actionType, dropzone);
        UnfreezeAllIcons(dropper.gameObject);
        DroppedOnPanel(dropper.gameObject);
        SpawnUI();
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
        }
        else
        {   
            // float maxValue = UISpawned.Max(x => x.transform.position.y);
            // Transform highestIcon = UISpawned.First(x => x.transform.position.y == maxValue).transform;
            Transform highestIcon= UISpawned[UISpawned.Count-1].transform;
            currSpawnedUI = Instantiate(
                prefabToSpawn, 
                highestIcon.transform.position+ Vector3.up*200f, 
                highestIcon.transform.rotation
                );
        }
        currSpawnedUI.transform.name=$"Icon {UISpawned.Count}";
        currSpawnedUI.transform.parent=this.transform;
        currSpawnedUI.transform.localScale=Vector3.one;
        UISpawned.Add(currSpawnedUI);
    }

    public void LevelUp(GameObject dropper)
    {
        Debug.Log("POTENZIAMENTOS");
        // dropper.GetComponent<UIIconController>().ResetPosition();
        UISpawned.Remove(dropper);
        Destroy(dropper);
    }

    public void DroppedOnPanel(GameObject dropper)
    {
        Debug.Log("Azione fatta");
        // dropper.GetComponent<UIIconController>().ResetPosition();
        UISpawned.Remove(dropper);
        Destroy(dropper);
    }

    public void FreezeAllIcons(GameObject toExclude){
        UISpawned.ForEach(
            x=>{
                x.GetComponent<UIIconController>().Freeze();
            }
        );
    }
    public void UnfreezeAllIcons(GameObject toExclude){
        UISpawned.ForEach(
            x=>{
                x.GetComponent<UIIconController>().Unfreeze();
            }
        );
        toExclude.GetComponent<UIIconController>().ResetPosition();
    }

}
