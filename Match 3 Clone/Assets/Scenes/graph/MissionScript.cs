using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionScript : MonoBehaviour
{
    public bool _play;
    public void playOn(){
        _play = true;
        GetComponent<Button>().interactable =true;
    }
    public void Play(){
        if(_play){
            string id = name.Split(' ')[2];
            GameObject SceneManager = GameObject.Find("SceneManager");
            SceneManager.GetComponent<SceneManagerScript>().SendMission(System.Convert.ToInt32(id));
            GameObject MM = GameObject.Find("MissionManager");
            MM.GetComponent<MissionManager>().startwindow(System.Convert.ToInt32(id));
        }
    }
}
