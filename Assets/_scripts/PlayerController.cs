using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject UIPanel;
    BoxCollider _boxCollider;
    Rigidbody _rigidbody;
    Animator _animator;


    private void Awake() {
        this._boxCollider=GetComponent<BoxCollider>();
        this._rigidbody=GetComponent<Rigidbody>();
        this._animator=GetComponent<Animator>();
        IconManager.PanelActionActivated+=OnPanelAction;
    }

    private void OnPanelAction(ActionTypes actionType, GameObject panel)
    {
        Debug.Log($"panel {panel.name} e this {this.UIPanel}");
        if (panel==this.UIPanel){
            switch (actionType)
            {
                case ActionTypes.jump:
                    Debug.Log($"The player {gameObject.name} is jumping");
                    _rigidbody.AddForce(Vector3.up*10,ForceMode.Impulse);
                    break;
                case ActionTypes.shoot:
                    Debug.Log($"The player {gameObject.name} is shooting");
                    break;
                default:
                    break;
            }
        }
    }

}
