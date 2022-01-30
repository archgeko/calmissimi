using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    string PlayerTag;
    BoxCollider _boxCollider;
    Rigidbody _rigidbody;


    private void Awake() {
        this._boxCollider=GetComponent<BoxCollider>();
        this._rigidbody=GetComponent<Rigidbody>();
        IconManager.PanelActionActivated+=OnPanelAction;
    }

    private void OnPanelAction(ActionTypes actionType, GameObject obj)
    {
        switch (actionType)
        {
            case ActionTypes.jump:
                Debug.Log($"The player {gameObject.name} is jumping");
                break;
            case ActionTypes.shoot:
                Debug.Log($"The player {gameObject.name} is shooting");
                break;
            default:
                break;
        }
    }

}
