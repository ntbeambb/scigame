using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public void StartPause(){
        //Debug.Log(Time.timeScale);
        Time.timeScale = 0;
    }
    public void EndPause(){
        Time.timeScale = 1;
    }
}
