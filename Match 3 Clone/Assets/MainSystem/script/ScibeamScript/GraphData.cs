using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "mission_Data", menuName = "mission/MissionData")]
public class GraphData : ScriptableObject
{
    public List<mission> CanPlay = new List<mission>();
    public List<mission> All = new List<mission>();
    public void Unlock(int _id_mission){
        CanPlay.Add(All[_id_mission].copy());
    }
}
[System.Serializable]
public class mission{
    public int id_mission;
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
        return ret;
    }
}
