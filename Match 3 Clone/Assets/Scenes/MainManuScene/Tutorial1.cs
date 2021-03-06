﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Tutorial1 : MonoBehaviour
{
    public GraphData grahpdata;
    public GameObject tutorial;
    public GameObject play;
    void Start()
    {
        if(File.Exists(Application.persistentDataPath + "/GraphData.save") == false){
            grahpdata.Save();
            tutorial.SetActive(true);
            return;
        }       
        grahpdata.Load();
        if(!grahpdata.TutorialStatus){
            tutorial.SetActive(true);
            play.GetComponent<Animator>().enabled = true;
        }
        return;
    }
}
