using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionManager : MonoBehaviour
{
    public GraphData Graphdata;
    public GameObject StartWindow;
    public GameObject Goalwin;
    public Sprite green;
    public Sprite blue;
    public List<GameObject> point = new List<GameObject>();
    void Start()
    {
        int co = Graphdata.CanPlay.Count;
        for(int i=0;i<co;i++){
            int po = Graphdata.CanPlay[i].id_mission;
            point[po].GetComponent<MissionScript>().playOn();
            if(Graphdata.CanPlay[i].finish){
                point[po].GetComponent<Image>().sprite = green;
            }else{
                point[po].GetComponent<Image>().sprite = blue;
            }
            
        }
    }
    public void startwindow(int id){
        TextMeshProUGUI textmesh = Goalwin.GetComponent<TextMeshProUGUI>();
        textmesh.text = "เป้าหมาย :"+Graphdata.CanPlay[id].Goal;
        StartWindow.SetActive(true);
    }
}
