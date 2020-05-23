using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragGameobject : MonoBehaviour
{
    public bool return_origin_position;
    public Vector2 origin_position;
    public InventorySystem backpack;
    private Vector2 temp;
    [SerializeField] private bool Inbox;
    [SerializeField] private string InboxName;
    private GameObject InboxObject;
    void Start(){
        origin_position = transform.position;
    }
    private void OnMouseDown(){
       // Debug.Log("mouse down");
    }
    private void OnMouseUp(){
       // Debug.Log("mouse up");
       bool ck = true;
        if(Inbox){
            InboxObject = GameObject.Find(InboxName);
            if(InboxObject.GetComponent<BoxCollider2D>().OverlapPoint(transform.position)){
                int id = GetComponent<ScibeamData>().ID();
                InboxObject.GetComponent<InboxGameobject>().GetItem(id);
                transform.parent.GetComponent<InventoryMinigame>().display();
                //Debug.Log("Workk");
            }
        }
        if(return_origin_position && ck){
            transform.position = origin_position;
        }
    }
    private void OnMouseDrag(){
       // Debug.Log("mouse drag");
        temp.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        temp.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        transform.position = temp;
    }
    void OnCollisionEnter2D(Collision2D other){
        Debug.Log("Inbox");
    }
}
