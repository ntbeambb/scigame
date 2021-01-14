using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New ProgressData", menuName = "Problem/ProgressData")]

public class ProgressData : ScriptableObject
{   
    private string path = "/DataProgress.save";
    [SerializeField] private float ProgressValue;
    public Subtask NowSub;
    public int level{get;private set;}
    public int mission_id;
    [SerializeField] private ProblemData OriginProblem;
    private int mav;
    public void Copy(ProblemData input){
        //Debug.Log("Copy");
        OriginProblem = input;
    }
    public void Reset(ProblemData input){
        Copy(input);
        ProgressValue = 0.0f;
        level = -1;
        NextSub();
    }
    private void NextSub(){
        level++;
        if(level < OriginProblem.problem.Length && OriginProblem.problem[level].Item.Count == 0)level++;
        //Debug.Log("Level "+level);
        //Debug.Log("Original Problem num " + OriginProblem.SubNum());
        if(level == OriginProblem.SubNum()){
            ProgressValue = 1.00f;
            return;
        }
        NowSub = OriginProblem.GetSub(level).Copy();//+++++
        mav = 0;
        for(int i=0;i<NowSub.Item.Count;i++){
            mav+=NowSub.Item[i].amount;
        }
        
    }
    private void CalProgress(int input){
        if(level == OriginProblem.SubNum())return;
        float LevelValue =0.99f/OriginProblem.SubNum();
        //Debug.Log("Level "+level+" LevelValue "+LevelValue);
        ProgressValue = LevelValue*level + LevelValue*(mav-input)/mav;
        

    }

    public Subtask GetSub(){
        return this.NowSub;
    }
    public float GetProgress(){
        return ProgressValue;
    }
    public int SendSub(Subtask input){
        NowSub = input;
        int l = NowSub.Item.Count;
        int ck = 0;
        int sum = 0;
        for(int i=0; i<l; i++){
            if(NowSub.Item[i].amount > 0)ck += 1<<i;
            sum += NowSub.Item[i].amount;
        }
        if(ck == 0){
            NextSub();
            sum = mav;
        }
        CalProgress(sum);
        //Debug.Log("Progress "+ProgressValue);
        return ck;
    }
    public void Save(){

        Basic_ProgressData send = new Basic_ProgressData();
        send.ProgressValue = ProgressValue;
        send.level = level;
        send.mav = mav;

        send.Subtask_IDObject = NowSub.IDObject;
        send.Subtask_SubText = NowSub.SubText;

        //array
        int size = NowSub.Item.Count;
        send.Subtask_Item_size = size;
        send.Subtask_Item_id = new int[size];
        send.Subtask_Item_amount = new int[size];
        for(int i=0;i<size;i++){
            send.Subtask_Item_id[i] = NowSub.Item[i].id;
            send.Subtask_Item_amount[i] = NowSub.Item[i].amount;
        }

        send.link_mission_id = mission_id;
        SaveSystem.SaveProgress(send,path);
    }
    public void Load(){
        Basic_ProgressData rev = SaveSystem.LoadProgress(path);
        ProgressValue = rev.ProgressValue;
        level = rev.level;
        mav = rev.mav;

        NowSub.IDObject = rev.Subtask_IDObject;
        NowSub.SubText = rev.Subtask_SubText;

        //List format
        int size = rev.Subtask_Item_size;
        NowSub.Item.Clear();
        for(int i=0;i<size;i++){
            NowSub.Item.Add(new slot(rev.Subtask_Item_id[i], "", rev.Subtask_Item_amount[i]));
        }

        mission_id = rev.link_mission_id;
    }
}

[System.Serializable]
public class Basic_ProgressData{
    public float ProgressValue;
    public int level;
    public int mav;

    //public Subtask NowSub;
        public int Subtask_IDObject;
        public string Subtask_SubText;
        //array data
        public int Subtask_Item_size;
        public int[] Subtask_Item_id;
        public int[] Subtask_Item_amount;

    //private ProblemData OriginProblem;
        //complex data
        public int link_mission_id;//from ProblemList
}
