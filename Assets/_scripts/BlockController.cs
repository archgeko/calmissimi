using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public float speed=1.0f;
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
        transform.position+=Vector3.back*speed*GameManager.gameSpeed;
    }

    private void OnTriggerEnter(Collider other){
        Debug.Log("Da cancellare");
        if (other.CompareTag("TriggerPoint")){
        }
    }

    
}
