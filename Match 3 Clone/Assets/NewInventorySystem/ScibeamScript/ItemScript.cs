using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemScript : MonoBehaviour,IPointerDownHandler,IBeginDragHandler,IEndDragHandler,IDragHandler,IDropHandler
{
    [SerializeField] private Canvas canvas;
    public int ID;
    private RectTransform rectTransform;

    private void Awake(){
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData evenData){
        //Debug.Log("OnBeginDrag");
    }
    public void OnDrag(PointerEventData evenData){
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += evenData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData evenData){
        //Debug.Log("OnEndDrag");
    }
    public void OnPointerDown(PointerEventData evenData){
        //Debug.Log("OnPointerDown");
    }
    public void OnDrop(PointerEventData evenData){

    }
}
