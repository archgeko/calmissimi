using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum DropzoneTypes{
    panel,
    action
}
public class Dropzone : MonoBehaviour, IDropHandler
{
    // public static Action <GameObject> DroppedOnPanel;
    // public static Action <GameObject> DroppedOnAction;
    private IconManager _iconManager;
    public DropzoneTypes dropzoneType;

    public static Action<GameObject, UIIconController> DicePanelEvent;
    public static Action<GameObject, UIIconController> DiceLevelupEvent;
    public static Action<GameObject, UIIconController> DiceLevelupFailEvent;
    private void Awake() {
        _iconManager=FindObjectOfType<IconManager>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropper=eventData.pointerDrag;
        if (dropper != null){
            UIIconController currDroppedUIController=dropper.GetComponent<UIIconController>();
            if (dropzoneType == DropzoneTypes.panel){
                DicePanelEvent?.Invoke(this.gameObject,currDroppedUIController);
            }
            else if (dropzoneType == DropzoneTypes.action){
                UIIconController currDropzoneUIController= GetComponent<UIIconController>();
                if (currDropzoneUIController.actionType==currDroppedUIController.actionType){
                    DiceLevelupEvent?.Invoke(this.gameObject,currDroppedUIController);
                }
                else {
                    DiceLevelupFailEvent?.Invoke(this.gameObject,currDroppedUIController);
                }
            }
        }
    }

}
