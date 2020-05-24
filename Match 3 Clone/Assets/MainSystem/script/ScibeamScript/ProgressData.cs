using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New ProgressData", menuName = "Problem/ProgressData")]
public class ProgressData : ScriptableObject
{
    [SerializeField] private float ProgressValue;
    public Subtask NowSub;
    private int level;
    [SerializeField] private ProblemData OriginProblem;
    private int mav;
    public void Copy(ProblemData input){
        //Debug.Log("Copy");
        OriginProblem = input;
        ProgressValue = 0.0f;
        level = -1;
        NextSub();
    }
    private void NextSub(){
        level++;
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
        return NowSub;
    }
    public float GetProgress(){
        return ProgressValue;
    }
    public void SendSub(Subtask input){
        NowSub = input;
        int l = NowSub.Item.Count;
        bool ck = true;
        int sum = 0;
        for(int i=0; i<l; i++){
            if(NowSub.Item[i].amount > 0)ck =false;
            sum += NowSub.Item[i].amount;
        }
        if(ck){
            NextSub();
            sum = mav;
        }
        CalProgress(sum);
        //Debug.Log("Progress "+ProgressValue);
    }
}
