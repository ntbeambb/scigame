using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragGameobject : MonoBehaviour
{
    public bool return_origin_position;
    public Vector2 origin_position;
    public InventorySystem backpack;
    private Vector2 temp;
    private GameObject InboxManager;
    private GameObject InboxObject;
    void Start(){
        origin_position = transform.position;
        InboxManager = GameObject.Find("InboxManager");
    }
    private void OnMouseDown(){
       // Debug.Log("mouse down");
    }
    private void OnMouseUp(){
       // Debug.Log("mouse up");
       bool ck = true;
        if(InboxManager.GetComponent<InboxManager>().GetInbox(transform.position) != null){

            InboxObject = InboxManager.GetComponent<InboxManager>().GetInbox(transform.position);
            int id = GetComponent<ScibeamData>().ID();

            InboxObject.GetComponent<InboxGameobject>().GetItem(id,1);

            transform.parent.GetComponent<InventoryMinigame>().display();
            //Debug.Log("Workk");
            
        }
        if(return_origin_position && ck){
            transform.position = origin_position;
            //Debug.Log("Origin "+transform.position);
        }
    }
    private void OnMouseDrag(){
       // Debug.Log("mouse drag");
        temp.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        temp.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        transform.position = temp;

        /*if(true){//optional effect
            

        }*/
        //codeee2
        //codeee11111111
        //code bug1
        //code bug2
    }
}
