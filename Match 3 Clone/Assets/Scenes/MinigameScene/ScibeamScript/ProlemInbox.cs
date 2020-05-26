using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProlemInbox : InboxGameobject
{
    public ProblemData problem;
    public ProgressData data;
    public InventorySystem backpack;
    public Subtask subtask;
    public GameObject progressbar;
    public GameObject Problemtext;
    private int nowsub;
    private float value;
    public bool Copy;
    void Start(){
        if(Copy)data.Copy(problem);
        subtask = data.GetSub();
        progressbar.GetComponent<Slider>().value = data.GetProgress();
        
        TextMeshProUGUI textmesh = Problemtext.GetComponent<TextMeshProUGUI>();
        //Debug.Log(problem.ProblemText);
        textmesh.text = problem.ProblemText;

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
    public override void GetItem(int _id){
        int po = CheckItem(_id);
        if(po != -1 ){
            if(subtask.Item[po].amount>0){
                //Debug.Log("Remove Item amount->"+subtask.Item[po].amount);
                backpack.RemoveItem(_id,1);
                subtask.Item[po].amount--;

                data.SendSub(subtask);
                progressbar.GetComponent<Slider>().value = data.GetProgress();
                //Debug.Log("Progress "+data.GetProgress());
                subtask = data.GetSub();
            }
        }
        
    }

}
