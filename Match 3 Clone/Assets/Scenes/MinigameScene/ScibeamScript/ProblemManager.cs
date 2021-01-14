using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ProblemManager : InboxGameobject
{
    [Header("Problem & Progress")]
    private ProblemData problem;
    public ProblemList ProList;
    public ProgressData data;
    public Subtask subtask;
    public GameObject progressbar;
    public GameObject Problemtext;
        //-input Object
        public List<QObject> InputList;

    [Header("Inventory")]
    public GameObject inventory;
    public InventorySystem backpack;

    [Header("General Data")]
    public SceneDataScript SceneData;
    public GraphData graphdata;
    public AllpicID Allpic;
    
    [Header("Prefab")]
    public GameObject InputPrefab;

    [Header("General Variable")]
    public GameObject input_group;
    public GameObject InboxManager;
    public GameObject quiz;
    private int nowsub;
    private float value;
    private int IDmission;
    public GameObject tutorial;
    public GameObject Background;
    public GameObject SendEffect;
    //public GameObject CorrectEffect;
    public bool Copy = false;

    [Header("Item Checker")]
    public List<GameObject> ItemChecker;
    public Sprite UnknownImage;
    public Sprite CompleteImage;
    public Sprite NoneImage;
    
    void Start(){
        graphdata.Save();
        graphdata.Load();
        backpack.Save();
    }
    public void ManualStart(){
        //link problem
        IDmission = SceneData.SendIdMission();
        problem = ProList.Plist[IDmission];

        //Load Data
        if(File.Exists(Application.persistentDataPath + "/DataInventory.save")){
            backpack.Load();
            }        
        //set up problem
        Copy = SceneData.CopyProblem;
        //Copy = true;/////////////////////////////////////////////////////////////////////
        SceneData.CopyProblem = false;
        if(Copy)data.Reset(problem);
        else{
            data.Load();
            data.Copy(problem);
        }
        data.mission_id = IDmission;
        subtask = data.GetSub();
        progressbar.GetComponent<Slider>().value = data.GetProgress();
        
        TextMeshProUGUI textmesh = Problemtext.GetComponent<TextMeshProUGUI>();
        //Debug.Log("Start "+subtask.SubText);
        textmesh.text = subtask.SubText;
        UpdateInput(data.level);
       

        CheckUpdate(false);

        //Tutorial
        if(!graphdata.TutorialStatus){
            tutorial.SetActive(true);
        }
    }
    private void UpdateInput(int SubID){
        List<GameObject> temp = new List<GameObject>();
        //generate GameInput
        Subtask sub  = problem.GetSub(SubID);
        InputList = sub.InputObject;
        for(int i = 0; i < InputList.Count;i++){
            temp.Add(CreateInput(InputList[i],i));
        }
        //Clear InputObject
        for(int i = 0;i<InboxManager.GetComponent<InboxManager>().inbox.Count;i++){
            if(InboxManager.GetComponent<InboxManager>().inbox[i] != null)Destroy(InboxManager.GetComponent<InboxManager>().inbox[i]);
        }
        InboxManager.GetComponent<InboxManager>().inbox.Clear();
        //Send Data to inboxManager
        InboxManager.GetComponent<InboxManager>().inbox = temp;
    }
    private GameObject CreateInput(QObject inp,int id){
        //create from prefab
        GameObject ret = Instantiate(InputPrefab,new Vector2(0f,0f), Quaternion.identity,input_group.transform) as GameObject;
        //change image
        ret.GetComponent<Image>().sprite = inp.Object;
        //set RectTransform
        ret.GetComponent<RectTransform>().localPosition = inp.localPosition;
        ret.GetComponent<RectTransform>().sizeDelta = inp.sizeDelta;
        //set Data
        ret.GetComponent<InputObjectScript>().InputID = id;
        ret.GetComponent<InputObjectScript>().Manager = this.gameObject;
        //set border
        ret.GetComponent<BoxCollider2D>().size = inp.sizeDelta;

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
        //remove Item
        int c = backpack.RemoveItem(_id,1);
        if(c==2){
            var invent = inventory.GetComponent<InventoryMinigame>();
            if(invent.StartPo >= backpack.container.Count)invent.StartPo = backpack.container.Count-1;
            inventory.GetComponent<InventoryMinigame>().display();
        }
        //sound effect
        SendEffect.GetComponent<AudioSource>().Play();
        //check object ID
        if(ObjId != subtask.IDObject)return;
        Debug.Log(_id.ToString()+" "+_amount.ToString()+" "+ObjId.ToString());
        int po = CheckItem(_id);
        if(po != -1 ){
            if(subtask.Item[po].amount>0){
                //Debug.Log("Remove Item amount->"+subtask.Item[po].amount);
                
                subtask.Item[po].amount--;
                
                int ck = data.SendSub(subtask);
                if(ck == 0 && data.level < problem.SubNum()){
                    UpdateInput(data.level);
                }
                var pro = data.GetProgress();
                progressbar.GetComponent<Slider>().value = pro;
                //Debug.Log("Progress "+data.GetProgress());
                subtask = data.GetSub();

                //update text
                TextMeshProUGUI textmesh = Problemtext.GetComponent<TextMeshProUGUI>();
                textmesh.text = subtask.SubText;
                
                //Debug.Log("GetItem "+subtask.SubText);
                //correct sound
                //CorrectEffect.GetComponent<AudioSource>().Play();

                if(pro == 1){
                    //
                    //Popup.SetActive(true);
                    backpack.Divide();
                    SceneData.PreIdMission = IDmission;
                    if(!graphdata.CanPlay[graphdata.PoMision(IDmission)].finish){
                        graphdata.Finish(IDmission);
                    }
                    quiz.GetComponent<QuizPlay>().StartQuiz();
                    Time.timeScale=0;
                    backpack.Save();
                    graphdata.Save();
                    CheckUpdate(true);
                    //graphdata.TutorialStatus = true;//////////////////temp offline
                }else{
                    CheckUpdate(false);
                }
            }
        }
    }
    public void CheckUpdate(bool ck){
        
        int level = data.level;
        int i;
        if(ck)level--;
        Subtask Osub = problem.GetSub(level);
        //Debug.Log("Level "+level);
        for(i = 0;i<Osub.Item.Count; i++){
            if(!ck && data.NowSub.Item[i].amount == Osub.Item[i].amount){
                ItemChecker[i].GetComponent<Image>().sprite = UnknownImage;
                ItemChecker[i].GetComponent<RectTransform>().sizeDelta = new Vector2(130,40);
            }else if(ck || data.NowSub.Item[i].amount == 0){
                ItemChecker[i].GetComponent<Image>().sprite = CompleteImage;
                ItemChecker[i].GetComponent<RectTransform>().sizeDelta = new Vector2(130,40);
            }else{
                ItemChecker[i].GetComponent<Image>().sprite = Allpic.ID[Osub.Item[i].id];
                ItemChecker[i].GetComponent<RectTransform>().sizeDelta = new Vector2(116.3f,29.4f);
            }
        }
        for(;i<3;i++){
            ItemChecker[i].GetComponent<Image>().sprite = NoneImage;
            ItemChecker[i].GetComponent<RectTransform>().sizeDelta = new Vector2(130,40);
        }
    }
    public void Tutorial2(){
        return;
    }
    public void SavePosition(){
        int c = problem.problem[data.level].InputObject.Count;
        for(int i=0;i<c;i++){
            problem.problem[data.level].InputObject[i].localPosition = InboxManager.GetComponent<InboxManager>().inbox[i].GetComponent<RectTransform>().localPosition;
            problem.problem[data.level].InputObject[i].sizeDelta = InboxManager.GetComponent<InboxManager>().inbox[i].GetComponent<RectTransform>().sizeDelta;
        }
        problem.StartBackground = new Vector2(Background.GetComponent<RectTransform>().localPosition.x,250);
    }
}