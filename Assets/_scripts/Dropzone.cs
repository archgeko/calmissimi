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

    private void Awake() {
        _iconManager=FindObjectOfType<IconManager>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropper=eventData.pointerDrag;
        if (dropper != null){
            if (dropzoneType == DropzoneTypes.panel){
                _iconManager.DroppedOnPanel(dropper);
            }
            else if (dropzoneType == DropzoneTypes.action){
                UIIconController currDropzoneUIController= GetComponent<UIIconController>();
                UIIconController currDroppedUIController=dropper.GetComponent<UIIconController>();
                if (currDropzoneUIController.actionType==currDroppedUIController.actionType){
                    _iconManager.LevelUp(dropper);
                }
                else {
                    currDroppedUIController.ResetPosition();
                }
            }
        }
    }

}
