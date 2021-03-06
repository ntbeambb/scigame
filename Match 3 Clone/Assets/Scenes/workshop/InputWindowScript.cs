﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputWindowScript : MonoBehaviour
{
    public GameObject inventory;
    public GameObject button;
    public GameObject ScView;
    RectTransform window;
    float height;
    float sty;
    float stx;
    float bty;

    public float offset;
    RectTransform rt;
    RectTransform bt;
    RectTransform sv;
    void Start()
    {
      //  window = GetComponent<RectTransform>();
      rt = GetComponent<RectTransform>();
      bt = button.GetComponent<RectTransform>();
      sv = ScView.GetComponent<RectTransform>();
      
      height = rt.rect.height;
      sty = rt.localPosition.y;
      stx = rt.localPosition.x;
      
      bty = bt.localPosition.y;
    }


    void Update()
    {
        float temp = inventory.GetComponent<RectTransform>().offsetMax.y;
        temp = offset-temp;
        
        rt.sizeDelta = new Vector2(rt.sizeDelta.x,height + temp);
        rt.localPosition = new Vector2(stx,sty - temp/2);
        bt.localPosition = new Vector2(bt.localPosition.x,bty + temp/2);

        sv.sizeDelta = new Vector2(sv.sizeDelta.x,height + temp - 40);
    }
}
