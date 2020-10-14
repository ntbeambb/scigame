﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class inventoryScript : MonoBehaviour
{
    public GameObject parent;
    public AllpicID Allpic;
    public GameObject prefab;
    public List<GameObject> AllItem;
    public InventorySystem Backpack;
    public GameObject workshop;

    private float st_y;
    public float size_y;
    public float diff_y;
    public float Itemsize;

    private GameObject CreateItem(int _id){
        Vector2 po = new Vector2(0,0);
        GameObject ret = Instantiate(prefab,po,Quaternion.identity,parent.transform) as GameObject;
        ret.GetComponent<ScibeamData>().id = _id;
        //add image
        var pic = ret.GetComponent<ItemWorkshopScript>().image;
        pic.GetComponent<Image>().sprite = Allpic.ID[_id];
        //resize
        var sx = Allpic.ID[_id].bounds.size.x / Itemsize;
        var sy = Allpic.ID[_id].bounds.size.y / Itemsize;
        pic.GetComponent<RectTransform>().localScale = new Vector2(sx,sy);
        //relocate
        var pox = pic.GetComponent<RectTransform>().position;
        pox.x += (sx-1f)/4f*pic.GetComponent<RectTransform>().sizeDelta.x;
        pic.GetComponent<RectTransform>().position = pox;
        return ret;

    }

    private void ReNum(GameObject inp,int num){
        inp.GetComponent<ItemWorkshopScript>().text.GetComponent<TextMeshProUGUI>().text = "X "+num.ToString();
    }

    void Start()
    {
        int l = Backpack.container.Count;
        
        resize();
        //additem
        for(int i=0;i<l;i++){
            AllItem.Add(CreateItem(Backpack.container[i].id));
        }
        Display(0);
    }
    private void resize(){
        int l = Backpack.container.Count;
        //resize
        var k = parent.GetComponent<RectTransform>().sizeDelta;
        k.y = l*size_y+(l+1)*diff_y;
        parent.GetComponent<RectTransform>().sizeDelta = k;
        st_y = parent.GetComponent<RectTransform>().sizeDelta.y/2-diff_y;
    }

    public void Display(int i){
        int l = Backpack.container.Count;
        resize();
        if(i>=l)i=l-1;
        if(i<0)i=0;
        for(;i<l;i++){
            AllItem[i].GetComponent<RectTransform>().localPosition = new Vector2(0,st_y-i*(diff_y+size_y));
            ReNum(AllItem[i],Backpack.container[i].amount);
        }
    }
    
    public void RemoveItem(int _id,GameObject element){
        int _amount = 1;
        int inp = Backpack.RemoveItem(_id,_amount);
        if(inp==2){
            //Debug.Log("destroy");
            AllItem.Remove(element);
            if(element!=null)Destroy(element);
        }
        workshop.GetComponent<WorshopScript>().GetItem(_id,_amount);
        Display(0);

    }
    public void AddItem(int _id,int _amount){
        int l = Backpack.container.Count;
        Backpack.AddItem(_id,"",_amount);
        if(Backpack.container.Count > l){
            AllItem.Add(CreateItem(Backpack.container[l].id));
            //resize();
        }
        Display(0);
    }


}
