using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public enum ActionTypes{
    jump,
    shoot
}
public class UIIconController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Canvas canvas;
    public Vector3 _startingPosition;
    public ActionTypes actionType;
    private RectTransform _rectTransform;
    private Rigidbody2D _rigidbody2D;
    private CanvasGroup _canvasGroup;

    public static Action AnyUIControllerGrabbed;
    public static Action AnyUIControllerDropped;

    private void Awake() {
        _rectTransform=GetComponent<RectTransform>();
        _rigidbody2D=GetComponent<Rigidbody2D>();
        _canvasGroup=GetComponent<CanvasGroup>();
        _startingPosition=this.transform.position;
        canvas=FindObjectOfType<Canvas>(); //OH NO - Test

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Start");
        Freeze();
        _canvasGroup.alpha=0.6f;
        _startingPosition=this.transform.position;
        _canvasGroup.blocksRaycasts=false;
        UIIconController.AnyUIControllerGrabbed?.Invoke();
    }
    public void Freeze(){
        _rigidbody2D.bodyType=RigidbodyType2D.Static;
    }
    public void Unfreeze(){
        _rigidbody2D.bodyType=RigidbodyType2D.Dynamic;
    }
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition+=eventData.delta/canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End");
        _canvasGroup.blocksRaycasts=true;
        _canvasGroup.alpha=1f;
        ResetPosition();
        //Unfreeze();
    }
    public void ResetPosition(){
        UIIconController.AnyUIControllerDropped?.Invoke();
        this.transform.position=this._startingPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Ciao");
    }

}
