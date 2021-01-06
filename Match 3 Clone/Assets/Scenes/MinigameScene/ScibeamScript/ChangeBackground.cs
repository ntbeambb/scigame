using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackground : MonoBehaviour
{
    public ProblemList Plist;
    public SceneDataScript Scenedata;
    public int idnow,preid;
    public int sp = 0;

    [SerializeField][Range(0.5f,2.5f)] float ChangeTime;

    private Vector2 st,fn;
    private float sttime;
    private bool alc = true;
    private float dark;

    public GameObject workshop;
    public GameObject inventory;
    public GameObject Manager;
    // Start is called before the first frame update
    void Start()
    {
        idnow = Scenedata.IdMission;
        preid = Scenedata.PreIdMission;
        sttime = Time.time;
        
        GetComponent<Image>().sprite = Plist.Plist[preid].Background;
            st = Plist.Plist[preid].StartBackground;
            fn = Plist.Plist[idnow].StartBackground;
        if(idnow == preid){
            GetComponent<Image>().sprite = Plist.Plist[idnow].Background;
            GetComponent<RectTransform>().anchoredPosition = fn;
            StartCoroutine(BackLoad());
            return;
        }    
        if(Plist.Plist[idnow].Background == Plist.Plist[preid].Background){
            sp = 1;
        }
        else sp=2;
        
        StartCoroutine(Change());
        
    }

    IEnumerator Change(){
        
        //new WaitForSeconds(ChangeTime + 0.1f);
        float per;
        while(Time.time < sttime + ChangeTime + 0.1f){
            per = (Time.time-sttime)/ChangeTime;
            //Debug.Log("per "+per.ToString());
            //Debug.Log(Time.time);
            if(sp == 1){
                GetComponent<RectTransform>().anchoredPosition = new Vector2(Mathf.Lerp(st.x,fn.x,per),Mathf.Lerp(st.y,fn.y,per));
            }else if(sp==2){
                if(per<0.5f){
                    if(alc){
                        GetComponent<Image>().sprite = Plist.Plist[preid].Background;
                        GetComponent<RectTransform>().anchoredPosition = st;
                        alc = false;
                    }
                    dark = 1f-(4f*per*per);
                    GetComponent<Image>().color = new UnityEngine.Color(dark, dark, dark, 1f);
                }else{
                    if(!alc){
                        GetComponent<Image>().sprite = Plist.Plist[idnow].Background;
                        GetComponent<RectTransform>().anchoredPosition = fn;
                        alc = true;
                    }
                    dark = (per-0.5f)*2f;
                    GetComponent<Image>().color = new UnityEngine.Color(dark, dark, dark, 1f);

                }

            }
            if(per >= 1 && sp!=0){
                sp=0;
                GetComponent<Image>().color = new Color32(255,255,255,255);
            }

            Vector2 tem = GetComponent<RectTransform>().anchoredPosition;
            tem.y = st.y;
            GetComponent<RectTransform>().anchoredPosition = tem;

            yield return new WaitForSeconds(0.01f);
        }
        End();
    }
    IEnumerator BackLoad(){
        float per;
        float Ctime = 0.5f;
        while(Time.time < sttime + Ctime + 0.1f){
            per = (Time.time-sttime)/Ctime;
            dark = (per*per);
            GetComponent<Image>().color = new UnityEngine.Color(dark, dark, dark, 1f);
            if(per >= 1 ){
                GetComponent<Image>().color = new Color32(255,255,255,255);
            }
            yield return new WaitForSeconds(0.01f);
        }
        End();
    }
    private void End(){
        Scenedata.PreIdMission = idnow;
        //start lab/inventory button
        workshop.SetActive(true);
        inventory.SetActive(true);
        //inventory.GetComponent<InventoryMinigame>().ManualStart();
        Manager.GetComponent<ProblemManager>().ManualStart();
    }
}