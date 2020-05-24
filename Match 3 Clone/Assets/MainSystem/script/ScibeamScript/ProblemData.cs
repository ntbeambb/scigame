﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Problem", menuName = "Problem/Problem")]
public class ProblemData : ScriptableObject
{   
    public string ProblemText;
    [SerializeField] private static int SubtaskAmount = 1;
    [SerializeField] private Subtask[] problem = new Subtask[SubtaskAmount];
    
    public int SubNum(){
        return problem.Length;
    }
    public Subtask GetSub(int _sub){
        if(_sub>=0 && _sub<problem.Length)return problem[_sub];
        return null;
    }
}
[System.Serializable]
public class Subtask{
    public static int ItemType;
    public List<slot> Item = new List<slot>();
    public Subtask Copy(){
        Subtask copy = new Subtask();
        //Debug.Log("Num of Slot "+copy.Item.Count);
        for(int i=0; i<this.Item.Count; i++){
            copy.Item.Add(this.Item[i].Copy());
        }
        return copy;
    }
}
