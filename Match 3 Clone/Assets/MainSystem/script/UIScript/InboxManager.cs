using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InboxManager : MonoBehaviour
{
    public List<GameObject> inbox = new List<GameObject>();
    public GameObject GetInbox(Vector2 po){
        for(int i = 0; i<inbox.Count;i++){
            if(inbox[i].GetComponent<BoxCollider2D>().OverlapPoint(po)){
                return inbox[i];
            }
        }
        return null;
    }
}
