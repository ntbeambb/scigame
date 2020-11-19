using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InputObjectScript : InboxGameobject
{
    //object data
    public int InputID;
    public GameObject Manager;
    public override void GetItem(int _id,int _amount){
        Manager.GetComponent<ProblemManager>().SendItem(_id,_amount,InputID);
    }
}
