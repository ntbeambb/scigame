using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProlemInbox : InboxGameobject
{
    private ProblemData problem;
    public ProblemList ProList;
    public ProgressData data;
    public InventorySystem backpack;
    public Subtask subtask;
    public GameObject progressbar;
    public GameObject Problemtext;
    public GameObject Popup;
    public SceneDataScript SceneData;
    public GraphData graphdata;
    private int nowsub;
    private float value;
    private int IDmission;
    public bool Copy;
    void Start(){
        IDmission = SceneData.SendIdMission();
        problem = ProList.Plist[IDmission-1];
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
                var pro = data.GetProgress();
                progressbar.GetComponent<Slider>().value = pro;
                //Debug.Log("Progress "+data.GetProgress());
                subtask = data.GetSub();

                if(pro == 1){
                    Popup.SetActive(true);
                    graphdata.Finish(IDmission);
                    Time.timeScale=0;
                }
            }
        }
        
    }

}
