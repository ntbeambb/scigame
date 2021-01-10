using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManage : MonoBehaviour
{
    public SoundScript data;
    public GameObject music;
    public void SetTime(){
        data.MusicTime = music.GetComponent<AudioSource>().time;
    }
    void Start(){
        if(SceneManager.GetActiveScene().name == "minigame" || SceneManager.GetActiveScene().name == "mainmanu" || SceneManager.GetActiveScene().name == "workshop"){
            data.MusicTime = 0;
        }
        music.GetComponent<AudioSource>().time = data.MusicTime;
        music.GetComponent<AudioSource>().Play();
    }
}
