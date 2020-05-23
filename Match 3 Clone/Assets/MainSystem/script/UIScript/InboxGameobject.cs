using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InboxGameobject : MonoBehaviour
{
    public ProblemData problem;
    public ProgressData data;
    public InventorySystem backpack;
    public Subtask subtask;
    public GameObject progressbar;
    private int nowsub;
    private float value;
    void Start(){
        data.Copy(problem);
        subtask = data.GetSub();
        progressbar.GetComponent<Slider>().value = data.GetProgress();

    }
    private int CheckItem(int input){
        int l = subtask.Item.Count;
        //Debug.Log("L "+l);
        for(int i=0; i<l; i++){
            if(subtask.Item[i].id == input){
                return i;
            }
        }
        return -1;
    }
    public void GetItem(int _id){
        int po = CheckItem(_id);
        if(po != -1 ){
            if(subtask.Item[po].amount>0){
                //Debug.Log("Remove Item amount->"+subtask.Item[po].amount);
                backpack.RemoveItem(_id,1);
                subtask.Item[po].amount--;

                data.SendSub(subtask);
                progressbar.GetComponent<Slider>().value = data.GetProgress();
                //Debug.Log("Progress "+data.GetProgress());
            }
        }
        
    }
}
