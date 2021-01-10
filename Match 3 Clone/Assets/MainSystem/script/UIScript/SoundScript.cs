using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New Sound", menuName = "Sound/Setting")]
public class SoundScript : ScriptableObject
{
    public AudioMixer mixer;
    public GraphData data;
    public float MusicTime;
    public bool EffectSet(bool inp){
        if(!inp){
            mixer.SetFloat("EffectVol",0f);
            data.EffectOn = false;
            return false;
        }else{
            mixer.SetFloat("EffectVol",1f);
            //mixer.ClearFloat("EffectVol");
            data.EffectOn = true;
            return true;
        }
    }
    public bool MusicSet(bool inp){
        if(!inp){
            mixer.SetFloat("MusicVol",0f);
            data.MusicOn = false;
            return false;
        }else{
            mixer.SetFloat("MusicVol",1f);
            //mixer.ClearFloat("MusicVol");
            data.MusicOn = true;
            return false;
        }
    }
}
