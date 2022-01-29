using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIIconController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform _rectTransform;
    private Rigidbody2D _rigidbody2D;

    private void Awake() {
        _rectTransform=GetComponent<RectTransform>();
        _rigidbody2D=GetComponent<Rigidbody2D>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Start");
        _rigidbody2D.bodyType=RigidbodyType2D.Static;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("is Dragging  ");
        _rectTransform.anchoredPosition+=eventData.delta;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End");
        _rigidbody2D.bodyType=RigidbodyType2D.Dynamic;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Ciao");
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
