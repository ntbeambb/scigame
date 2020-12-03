using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProblemManager : InboxGameobject
{
    //problem
    private ProblemData problem;
    public ProblemList ProList;
    public ProgressData data;
    public Subtask subtask;
    public GameObject progressbar;
    public GameObject Problemtext;
        //-input Object
        public List<QObject> InputList;
    //inventory
    public GameObject inventory;
    public InventorySystem backpack;
    //data
    public SceneDataScript SceneData;
    public GraphData graphdata;
    //quiz
    public GameObject quiz;
    //prefab
    public GameObject InputPrefab;
    //general variable
    public GameObject input_group;
    public GameObject InboxManager;
    private int nowsub;
    private float value;
    private int IDmission;
    public bool Copy = false;
    
    public void ManualStart(){
        //link problem
        IDmission = SceneData.SendIdMission();
        problem = ProList.Plist[IDmission];
        
        //set up problem
        Copy = SceneData.CopyProblem;
        SceneData.CopyProblem = false;
        if(Copy)data.Copy(problem);

        subtask = data.GetSub();
        progressbar.GetComponent<Slider>().value = data.GetProgress();
        
        TextMeshProUGUI textmesh = Problemtext.GetComponent<TextMeshProUGUI>();
        //Debug.Log("Start "+subtask.SubText);
        textmesh.text = subtask.SubText;

        List<GameObject> temp = new List<GameObject>();
        //generate GameInput
        InputList = problem.InputObject;
        for(int i = 0; i < InputList.Count;i++){
            temp.Add(CreateInput(InputList[i],i));
        }
        //Send Data to inboxManager
        InboxManager.GetComponent<InboxManager>().inbox = temp;
    }
    private GameObject CreateInput(QObject inp,int id){
        //create from prefab
        GameObject ret = Instantiate(InputPrefab,new Vector2(0f,0f), Quaternion.identity,input_group.transform) as GameObject;
        //change image
        ret.GetComponent<Image>().sprite = inp.Object;
        //set position
        ret.transform.localPosition = inp.LocalPosition;
        //set Data
        ret.GetComponent<InputObjectScript>().InputID = id;
        ret.GetComponent<InputObjectScript>().Manager = this.gameObject;
        ret.name = "Input Object "+ id.ToString();
        
        return ret;
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
    public void SendItem(int _id,int _amount,int ObjId){
        //check object ID
        if(ObjId != subtask.IDObject)return;
        Debug.Log(_id.ToString()+" "+_amount.ToString()+" "+ObjId.ToString());
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

                //update text
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