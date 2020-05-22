using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragGameobject : MonoBehaviour
{
    public bool return_origin_position;
    public Vector2 origin_position;
    private Vector2 temp;
    void Start(){
        origin_position = transform.position;
    }
    private void OnMouseDown(){
       // Debug.Log("mouse down");
    }
    private void OnMouseUp(){
       // Debug.Log("mouse up");
        if(false){


        }else if(return_origin_position){
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
