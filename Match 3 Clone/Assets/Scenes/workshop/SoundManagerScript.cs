using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManagerScript : MonoBehaviour
{
    public GraphData data;
    public SoundScript sound;
    public Sprite On;
    public Sprite Off;
    public GameObject MusicButton;
    public GameObject EffectButton;
    void Start(){
        if(!data.MusicOn){
            MusicButton.GetComponent<Image>().sprite = Off;
        }
        sound.MusicSet(data.MusicOn);
        sound.EffectSet(data.EffectOn);
        if(!data.EffectOn){
            EffectButton.GetComponent<Image>().sprite = Off;
        }
    }
    public void MusicToggle(){
        if(data.MusicOn){
            sound.MusicSet(false);
            MusicButton.GetComponent<Image>().sprite = Off;
        }else{
            sound.MusicSet(true);
            MusicButton.GetComponent<Image>().sprite = On;
        }
    }
    public void EffectToggle(){
        if(data.EffectOn){
            sound.EffectSet(false);
            EffectButton.GetComponent<Image>().sprite = Off;
        }else{
            sound.EffectSet(true);
            EffectButton.GetComponent<Image>().sprite = On;
        }
    }
}
