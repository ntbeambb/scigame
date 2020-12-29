using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Problem", menuName = "Problem/Problem")]
public class ProblemData : ScriptableObject
{   
    public string ProblemText;
    public List<int> FoundElement;
    public Sprite Background;
    public Vector2 StartBackground;
    public List<QObject> InputObject = new List<QObject>();
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
    public int IDObject;
    [SerializeField] public string SubText;
    public List<slot> Item = new List<slot>();

    public Subtask Copy(){
        Subtask copy = new Subtask();
        //Debug.Log("Num of Slot "+copy.Item.Count);
        copy.SubText = this.SubText;
        copy.IDObject = this.IDObject;
        for(int i=0; i<this.Item.Count; i++){
            copy.Item.Add(this.Item[i].Copy());
        }
        return copy;
    }
}
[System.Serializable]
public class QObject{
    public Vector2 LocalPosition;
    public Sprite Object;
}
