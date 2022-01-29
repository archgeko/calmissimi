using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float gameSpeed=1.0f;
    public static void SetGameSpeed(float newGameSpeed){
        gameSpeed=newGameSpeed;
    }

    public Action GameStarted;
    public Action GameEnded;

    [Button]
    public void StartGame()
    {
        GameStarted?.Invoke();
        Debug.Log("Partita Cominciata");
    }

    public void EndGame()
    {
        GameEnded?.Invoke();
        Debug.Log("Partita Finita");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
