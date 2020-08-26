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
    public List<GameObject> point = new List<GameObject>();
    void Start()
    {
        int co = Graphdata.CanPlay.Count;
        for(int i=0;i<co;i++){
            int po = Graphdata.CanPlay[i].id_mission-1;
            point[po].GetComponent<MissionScript>().playOn();
            point[po].GetComponent<Image>().color = new Color32(0,255,30,255);
        }
    }
    public void startwindow(int id){
        TextMeshProUGUI textmesh = Goalwin.GetComponent<TextMeshProUGUI>();
        //textmesh.text = 'เป้าหมาย :';
        StartWindow.SetActive(true);
    }
}
