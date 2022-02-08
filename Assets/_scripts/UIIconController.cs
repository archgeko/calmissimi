using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;


public enum ActionTypes{
    jump,
    shoot,
    cista
}

public class UIIconController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Canvas canvas;
    public RectTransform _canvasRect;
    public List<Color> levelUpColorsList;
    public Vector3 _startingPosition;
    public ActionTypes actionType;
    public int _maxLevel;
    public int _currentLevel;
    private RectTransform _rectTransform;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    private CanvasGroup _canvasGroup;

    public static Action <GameObject> AnyUIControllerGrabbed;
    public static Action <GameObject> AnyUIControllerDropped;

    private void Awake() {
        _rectTransform=GetComponent<RectTransform>();
        _rigidbody2D=GetComponent<Rigidbody2D>();
        _boxCollider2D=GetComponent<BoxCollider2D>();
        _canvasGroup=GetComponent<CanvasGroup>();
        _startingPosition=this.transform.position;
        canvas=FindObjectOfType<Canvas>(); //OH NO - Test
        _canvasRect=canvas.GetComponent<RectTransform>();
        _maxLevel=2;
        _currentLevel=0;

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"Start Drag {gameObject.name}");
        Freeze();
        _canvasGroup.alpha=0.6f;
        _startingPosition=this.transform.position;
        _canvasGroup.blocksRaycasts=false;
        UIIconController.AnyUIControllerGrabbed?.Invoke(this.gameObject);
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
        Debug.Log($"Start End {gameObject.name}");
        _canvasGroup.blocksRaycasts=true;
        _canvasGroup.alpha=1f;
        UIIconController.AnyUIControllerDropped?.Invoke(this.gameObject);

    }
    public void ResetPosition(){
        this.transform.position=this._startingPosition;
    }
    public void LevelUpAndUpdateGraphics(){
        _currentLevel=_currentLevel+1>_maxLevel? _maxLevel:_currentLevel+1;
        UpdateIconGraphics();
    }

    private void UpdateIconGraphics(){
        Image currImage= gameObject.GetComponent<Image>();
        currImage.color=  levelUpColorsList.ElementAt(_currentLevel);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    void FixedUpdate()
    {
        // _boxCollider2D.offset = new Vector2(0, 0);
        // Vector3 newHeight= Vector3.Scale(_rectTransform.localScale, _canvasRect.localScale).y;
        // _boxCollider2D.size = new Vector2();
    }
}
