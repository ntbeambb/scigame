using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Problem", menuName = "Problem/Problem")]
public class ProblemData : ScriptableObject
{   
    public string ProblemText;
    public List<int> FoundElement;

    [Header("Background")]
    public Sprite Background;
    public Vector2 StartBackground;

    [Header("problem")]
    [SerializeField] private static int SubtaskAmount = 1;
    [SerializeField] public Subtask[] problem = new Subtask[SubtaskAmount];
    
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
    [Header("Data")]
    public int IDObject;
    [SerializeField] public string SubText;
    [Header("Item & input")]
    public List<QObject> InputObject = new List<QObject>();
    public List<slot> Item = new List<slot>();

    public Subtask Copy(){
        Subtask copy = new Subtask();
        //Debug.Log("Num of Slot "+copy.Item.Count);
        copy.SubText = this.SubText;
        copy.IDObject = this.IDObject;
        for(int i=0; i<this.Item.Count; i++){
            copy.Item.Add(this.Item[i].Copy());
        }
        for(int i=0; i<this.InputObject.Count; i++){
            copy.InputObject.Add(this.InputObject[i].Copy());
        }
        return copy;
    }
}
[System.Serializable]
public class QObject{
    public Vector2 localPosition;
    public Vector2 sizeDelta;
    public Sprite Object;
    public QObject Copy(){
        QObject ret = new QObject();
        ret.localPosition = this.localPosition;
        ret.sizeDelta = this.sizeDelta;
        ret.Object = this.Object;
        return ret;
    }
}
