using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "mission_Data", menuName = "mission/MissionData")]
public class GraphData : ScriptableObject
{
    private string path = "/GraphData.save";
    public List<mission> CanPlay = new List<mission>();
    public List<int> Pass = new List<int>();
    public List<mission> All = new List<mission>();
    public bool TutorialStatus;
    public int PoMision(int id){
        int temp = CanPlay.Count;
        for(int i=0;i<temp;i++){
            if(CanPlay[i].id_mission==id)return i;
        }
        return -1;
    }
    public void Unlock(int _id_mission){
        CanPlay.Add(All[_id_mission].copy());
    }
    public void Finish(int _IDmission){
        int po = PoMision(_IDmission);
        int l = CanPlay[po].unlock.Count;
        CanPlay[po].finish = true;
        Pass.Add(po);
        for(int i=0;i<l;i++ ){
            Unlock(CanPlay[po].unlock[i]);
        }
    }
    public void Save(){
        Basic_GraphData send = new Basic_GraphData();
        send.TutorialStatus = TutorialStatus;
        //array
        int size = CanPlay.Count;
        send.size = size;
        send.CanPlay = new int[size];
        send.CanPlay_finish = new int[size];
        for(int i = 0;i<size;i++){
            send.CanPlay[i] = CanPlay[i].id_mission;
            if(CanPlay[i].finish){
                send.CanPlay_finish[i] = 1;
            }else send.CanPlay_finish[i] = 0;
        }
        SaveSystem.SaveGraphData(send,path);
    }
    public void Load(){
        Basic_GraphData rev = SaveSystem.LoadGraphData(path);
        TutorialStatus = rev.TutorialStatus;
        CanPlay.Clear();
        int size = rev.size;
        for(int i=0;i<size;i++){
            Unlock(rev.CanPlay[i]);
            if(rev.CanPlay_finish[i] == 1){
                CanPlay[i].finish = true;
            }
        }
    }
}
[System.Serializable]
public class mission{
    public int id_mission;
    public string Goal;
    public bool finish;
    public List<int> unlock = new List<int>();
    public mission copy(){
        mission ret = new mission();
        ret.id_mission = this.id_mission;
        List<int> send = new List<int>();
        int co = this.unlock.Count;
        ret.Goal = this.Goal;
        for(int i=0;i<co;i++){
            send.Add(this.unlock[i]);
        }
        ret.unlock = send;
        ret.finish = false;
        return ret;
    }
}
[System.Serializable]
public class Basic_GraphData{
    public int size;
    public int[] CanPlay;
    public int[] CanPlay_finish;
    public bool TutorialStatus;
}
