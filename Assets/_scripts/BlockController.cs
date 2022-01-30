using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    private ChainController _chainController;
    public float speed=1.0f;
    public static Action<BlockController> BlockCollided;
    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move(){
        transform.position+=Vector3.back*speed*GameManager.gameSpeed*Time.deltaTime;
    }
    public void SetChainController(ChainController chainController){
        this._chainController=chainController;
    }
    private void OnTriggerExit(Collider other){
        if (other.CompareTag("TriggerPoint")){
            other.transform.parent.GetComponent<ChainController>().AddBlockToRepository(this.gameObject);
            other.transform.parent.GetComponent<ChainController>().AddRandomToCarpet();
        }
    }
    

    
}
