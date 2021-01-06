using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionManager : MonoBehaviour
{
    public GraphData Graphdata;
    public ProblemList Plist;
    public AllpicID Allpic;
    public GameObject tutorial;
    public GameObject StartWindow;
    public GameObject Goalwin;
    public GameObject Level;
    public Sprite green;
    public Sprite blue;
    public List<GameObject> point = new List<GameObject>();
    public List<GameObject> ElementList;
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
        if(!Graphdata.TutorialStatus){
            tutorial.SetActive(true);
        }
    }
    public void startwindow(int id){
        TextMeshProUGUI textmesh = Goalwin.GetComponent<TextMeshProUGUI>();
        textmesh.text = Graphdata.CanPlay[id].Goal;
        TextMeshProUGUI level = Level.GetComponent<TextMeshProUGUI>();
        
        level.text = (id+1).ToString();
        List<int> FoundElement = Plist.Plist[id].FoundElement;
        for(int i = 0;i<FoundElement.Count;i++){
            ElementList[i].SetActive(true);
            //change image
            ElementList[i].GetComponent<Image>().sprite = Allpic.ID[FoundElement[i]];  
        }

        StartWindow.SetActive(true);
    }
    public void CloseWindow(){
        StartWindow.SetActive(false);
    }
}
