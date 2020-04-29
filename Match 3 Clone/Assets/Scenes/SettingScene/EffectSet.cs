using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSet : MonoBehaviour
{
    public GameObject indicator;
    public bool effect_on;
    public float movespeed;
    private float t = 0.0f;
    private bool switching =false;
    private float onX = 18.5f;
    private float offX = -18.5f;
    private float Actx;
    private RectTransform toggle;
    private RectTransform handle;

    public void Awake(){
            toggle = indicator.GetComponent<RectTransform>();
    }

    public void ToggleEffect(){
     //   Debug.Log("Debugg");
        if(effect_on){
            effect_on=false;
            switching = true;
        }else {
            effect_on=true;
            switching = true;
        }
        

    }
    void Update(){
  //      Actx=toggle.sizeDelta.x;
        if(switching){
            if(effect_on){
                toggle.localPosition = MoveIndicator(indicator,offX,onX);
            }else{
                toggle.localPosition = MoveIndicator(indicator,onX,offX);
            }
            
        }
       // Debug.Log("Say Hi");
    }
    private Vector3 MoveIndicator(GameObject movehand,float startX,float endX){
        Vector3 position = new Vector3(Mathf.Lerp(startX,endX,t+=movespeed*Time.deltaTime),0,0);
        if(t>1){
            t=0;
            switching = false;

        }
        return position;

    }
}
