using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New SceneData", menuName = "New Data System/Scene")]
public class SceneDataScript : ScriptableObject
{
    [SerializeField]private static int MaxScene = 10;
    [SerializeField]private string[] SceneStack = new string[MaxScene];
    public int NumScene = 0;
    public int IdMission;
    public void ResetScene(){
        for(int i=0;i<MaxScene;i++){
            SceneStack[i]=null;
        }
        NumScene = 0;
    }
    public void PushScene(string NewScene){
        if(NumScene<MaxScene){
            SceneStack[NumScene] = NewScene;
            NumScene++;
        }
    }
    public string PopScene(){
        if(NumScene>0){
            string temp = SceneStack[NumScene-1];
            SceneStack[NumScene-1] = null;
            NumScene--;
            return temp;
        }
        return null;
    }
    public string PreScene(){
        if(NumScene>0){
            return SceneStack[NumScene-1];
        }
        return null;
    }
}
