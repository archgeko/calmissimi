using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using System.Linq;

public class ChainController : MonoBehaviour
{
    public float speed;

    public string chainTag;
    public GameObject carpet;
    public GameObject repository;
    public List<GameObject> blockPrefabList;
    public GameManager gameManager;
    public int numberToSpawn;

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
            SetAllChildSpeed();
        }
    }
    
    void Awake()
    {
        this.SpawnBlocks();
        this.gameManager=FindObjectOfType<GameManager>();
        gameManager.GameStarted+=OnStartGame;
        gameManager.GameStarted+=OnEndGame;
    }

    private void OnEndGame()
    {
        return;
    }


    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnStartGame()
    {
        SpawnBlocks();
        for (int i = 0; i < this.numberToSpawn; i++)
        {
            AddRandomToCarpet();
        }
        Speed=5f;
    }

    [Button]
    public void SpawnBlocks()
    {
        for (int i = 0; i < this.numberToSpawn; i++)
        {
            int randomIndex= UnityEngine.Random.Range(0,blockPrefabList.Count);
            GameObject currBlock = Instantiate(
                blockPrefabList[randomIndex],
                this.transform
            );
            this.AddToChainPart(currBlock, this.repository);
        }
    }
    public void AddToChainPart(GameObject blockGO, GameObject chainPart, bool randomize = true, int displacement=0)
    {
        if (randomize)
        {
            Vector3 newScale = blockGO.transform.localScale;
            if (UnityEngine.Random.Range(0, 2) == 1)
            {
                newScale.x = newScale.x = -1;
            }
            if (UnityEngine.Random.Range(0, 2) == 1)
            {
                newScale.z = newScale.z = -1;
            }
            blockGO.transform.localScale = newScale;
        }

        List<GameObject> chainpartBlocks = new List<GameObject>();
        foreach (Transform tr in chainPart.transform)
        {
            chainpartBlocks.Add(tr.gameObject);
        }
        blockGO.transform.parent = chainPart.transform;

        if (chainpartBlocks.Count > 0)
        {
            blockGO.transform.position = chainpartBlocks[chainpartBlocks.Count - 1].transform.position + Vector3.forward * displacement;
        }
        else
        {
            blockGO.transform.position = chainPart.transform.position;
        }
    }
    [Button]
    public void AddRandomToCarpet()
    {
        List<GameObject> repositoryBlocks = new List<GameObject>();
        foreach (Transform tr in repository.transform)
        {
            repositoryBlocks.Add(tr.gameObject);
        }
        repositoryBlocks = repositoryBlocks.OrderBy(x => Guid.NewGuid()).ToList();

        repositoryBlocks[0].gameObject.GetComponent<BlockController>().speed=speed;
        this.AddToChainPart(repositoryBlocks[0], this.carpet,true,10);
    }

    public void AddBlockToRepository(GameObject blockToRepository)
    {
        blockToRepository.gameObject.GetComponent<BlockController>().speed=0;
        this.AddToChainPart(blockToRepository, this.repository);
    }

    public List<Transform> GetAllChild(){
        List<Transform> allChild=new List<Transform>();
        foreach (Transform tr in repository.transform)
        {
            allChild.Add(tr);
        }
        foreach (Transform tr in carpet.transform)
        {
            allChild.Add(tr);
        }
        return allChild;
    }
    
    public void SetAllChildSpeed()
    {
        foreach (Transform tr in repository.transform)
        {
            tr.gameObject.GetComponent<BlockController>().speed=0;
        }
        foreach (Transform tr in carpet.transform)
        {
            tr.gameObject.GetComponent<BlockController>().speed=speed;
        }
    }
}
