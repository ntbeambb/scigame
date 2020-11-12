using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProlemManager : InboxGameobject
{
    //problem
    private ProblemData problem;
    public ProblemList ProList;
    public ProgressData data;
    public Subtask subtask;
    public GameObject progressbar;
    public GameObject Problemtext;
        //-input Object
        public List<QObject> InputList;
    //inventory
    public GameObject inventory;
    public InventorySystem backpack;
    //data
    public SceneDataScript SceneData;
    public GraphData graphdata;
    //quiz
    public GameObject quiz;
    //prefab
    public GameObject InputPrefab;
    //general variable
    private int nowsub;
    private float value;
    private int IDmission;
    public bool Copy;
    void Start(){
        //link problem
        IDmission = SceneData.SendIdMission();
        problem = ProList.Plist[IDmission];
        
        //set up problem
        Copy = true;
        if(Copy)data.Copy(problem);
        subtask = data.GetSub();
        progressbar.GetComponent<Slider>().value = data.GetProgress();
        
        TextMeshProUGUI textmesh = Problemtext.GetComponent<TextMeshProUGUI>();
        //Debug.Log("Start "+subtask.SubText);
        textmesh.text = subtask.SubText;

        //generate GameInput
        InputList = problem.InputObject;

    }
    private GameObject CreateInput(){
        GameObject ret = Instantiate(InputPrefab,new Vector2(0f,0f), Quaternion.identity) as GameObject;

    }
    private int CheckItem(int input){
        int l = subtask.Item.Count;
        //Debug.Log("L "+l);
        for(int i=0; i<l; i++){
            if(subtask.Item[i].id == input){
                return i;
            }
        }
        return -1;
    }
}
