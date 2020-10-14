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
    public GameObject quiz;
    public GameObject inventory;
    public SceneDataScript SceneData;
    public GraphData graphdata;
    private int nowsub;
    private float value;
    private int IDmission;
    public bool Copy;
    void Start(){
        IDmission = SceneData.SendIdMission();
        problem = ProList.Plist[IDmission];
        
        if(graphdata.CanPlay[graphdata.PoMision(IDmission)].finish){
            graphdata.CanPlay[graphdata.PoMision(IDmission)].finish = false;
            Copy=true;
        }else Copy=false;
        if(Copy)data.Copy(problem);
        subtask = data.GetSub();
        progressbar.GetComponent<Slider>().value = data.GetProgress();
        
        TextMeshProUGUI textmesh = Problemtext.GetComponent<TextMeshProUGUI>();
       //Debug.Log("Start "+subtask.SubText);
        textmesh.text = subtask.SubText;

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
    public override void GetItem(int _id,int _amount){
        int po = CheckItem(_id);
        if(po != -1 ){
            if(subtask.Item[po].amount>0){
                //Debug.Log("Remove Item amount->"+subtask.Item[po].amount);
                int c = backpack.RemoveItem(_id,1);
                subtask.Item[po].amount--;
                if(c==2){
                    var invent = inventory.GetComponent<InventoryMinigame>();
                    if(invent.StartPo >= backpack.container.Count)invent.StartPo = backpack.container.Count-1;
                    inventory.GetComponent<InventoryMinigame>().display();
                }
                data.SendSub(subtask);
                var pro = data.GetProgress();
                progressbar.GetComponent<Slider>().value = pro;
                //Debug.Log("Progress "+data.GetProgress());
                subtask = data.GetSub();

                //upgate text
                TextMeshProUGUI textmesh = Problemtext.GetComponent<TextMeshProUGUI>();
                textmesh.text = subtask.SubText;
                //Debug.Log("GetItem "+subtask.SubText);
                
                if(pro == 1){
                    //
                    //Popup.SetActive(true);
                    SceneData.PreIdMission = IDmission;
                    graphdata.Finish(IDmission);
                    quiz.GetComponent<QuizPlay>().StartQuiz();
                    Time.timeScale=0;
                }
            }
        }
        
    }

}
