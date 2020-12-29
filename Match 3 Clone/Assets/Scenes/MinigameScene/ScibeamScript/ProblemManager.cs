﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public bool Copy = false;

    [Header("Item Checker")]
    public List<GameObject> ItemChecker;
    public Sprite UnknownImage;
    public Sprite CompleteImage;
    public Sprite NoneImage;
    
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
        Copy = true;/////////////////////////////////////////////////////////////////////
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

        List<GameObject> temp = new List<GameObject>();
        //generate GameInput
        InputList = problem.InputObject;
        for(int i = 0; i < InputList.Count;i++){
            temp.Add(CreateInput(InputList[i],i));
        }
        //Send Data to inboxManager
        InboxManager.GetComponent<InboxManager>().inbox = temp;

        CheckUpdate(false);

        //Tutorial
        /*if(!graphdata.TutorialStatus){
            tutorial.SetActive(true);
        }*/
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
                    if(!graphdata.CanPlay[graphdata.PoMision(IDmission)].finish){
                        graphdata.Finish(IDmission);
                    }
                    quiz.GetComponent<QuizPlay>().StartQuiz();
                    Time.timeScale=0;
                    backpack.Save();
                    graphdata.Save();
                    CheckUpdate(true);
                }else{
                    CheckUpdate(false);
                }
            }
        }
    }
    public void CheckUpdate(bool ck){
        int level = data.level;
        if(ck)level--;
        Subtask Osub = problem.GetSub(level);
        for(int i = 0;i<Osub.Item.Count; i++){
            ItemChecker[i].SetActive(true);
            if(!ck && Osub.Item[i].amount == 0){
                ItemChecker[i].GetComponent<Image>().sprite = NoneImage;
            }else if(!ck && data.NowSub.Item[i].amount == Osub.Item[i].amount){
                ItemChecker[i].GetComponent<Image>().sprite = UnknownImage;
            }else if(ck || data.NowSub.Item[i].amount == 0){
                ItemChecker[i].GetComponent<Image>().sprite = CompleteImage;
            }else{
                ItemChecker[i].GetComponent<Image>().sprite = Allpic.ID[Osub.Item[i].id];
            }
        } 
    }
    public void Tutorial2(){
        return;
    }
    public void SavePosition(){
        int c = problem.InputObject.Count;
        for(int i=0;i<c;i++){
            problem.InputObject[i].LocalPosition = InboxManager.GetComponent<InboxManager>().inbox[i].GetComponent<RectTransform>().localPosition;
        }
        problem.StartBackground = new Vector2(Background.GetComponent<RectTransform>().localPosition.x,250);
    }
}