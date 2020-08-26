using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookScript : InboxGameobject
{   
    public GameObject book;
    public void OpenBook(int page){
        book.SetActive(true);
    }
    public void CloseBook(){
        book.SetActive(false);
    }
    /*private void OnMouseDown(){
       OpenBook(0);
    }*/
    public override void GetItem(int _id){
        OpenBook(_id);
    }
}
