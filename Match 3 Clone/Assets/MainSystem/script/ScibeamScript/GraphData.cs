﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "mission_Data", menuName = "mission/MissionData")]
public class GraphData : ScriptableObject
{
    public List<mission> CanPlay = new List<mission>();
    public List<mission> All = new List<mission>();
    public List<int> Pass = new List<int>();
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
        for(int i=0;i<co;i++){
            send.Add(this.unlock[i]);
        }
        ret.unlock = send;
        ret.finish = false;
        return ret;
    }
}
