using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputMix : MonoBehaviour
{
    public GameObject parent;
    public AllpicID Allpic;
    public GameObject prefab;
    public List<GameObject> AllItem;
    public GameObject workshop;
    public GameObject inventory;

    private List<int> inputID;
    private float st_y;
    public float size_y;
    public float diff_y;
    public float Itemsize;
    public float stx_pic;
    public float size;
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
        pox.x += (sx-1f)/4f*55;//pic.GetComponent<RectTransform>().sizeDelta.x;
        pic.GetComponent<RectTransform>().position = pox;
        //Debug.Log("in Po "+pox.x);
        return ret;

    }
    private void ReNum(GameObject inp,int num){
        inp.GetComponent<ItemWorkshopScript>().text.GetComponent<TextMeshProUGUI>().text = "X "+num.ToString();
    }
    /*void Start()
    {
        int l = workshop.GetComponent<WorshopScript>().inputID.Count;
        inputID = workshop.GetComponent<WorshopScript>().inputID;
        //additem
        for(int i=0;i<l;i++){
            AllItem.Add(CreateItem(inputID[i]));
        }
        Display(0);
    }*/
    public void resize(int l){
        //resize
        var k = parent.GetComponent<RectTransform>().sizeDelta;
        k.y = l*size_y+(l+1)*diff_y;
        parent.GetComponent<RectTransform>().sizeDelta = k;
        //st_y = parent.GetComponent<RectTransform>().sizeDelta.y/2-diff_y;
    }
    private GameObject mini(GameObject inp){
        var k = inp.GetComponent<RectTransform>().sizeDelta;
        k.y = 50;
        inp.GetComponent<RectTransform>().sizeDelta = k;

        k = inp.GetComponent<ItemWorkshopScript>().text.GetComponent<RectTransform>().localPosition;
        k.x = 70;
        inp.GetComponent<ItemWorkshopScript>().text.GetComponent<RectTransform>().localPosition = k;
        

        k = inp.GetComponent<ItemWorkshopScript>().image.GetComponent<RectTransform>().localPosition;
        k.x += stx_pic;
        inp.GetComponent<ItemWorkshopScript>().image.GetComponent<RectTransform>().localPosition = k;
        inp.GetComponent<ItemWorkshopScript>().image.GetComponent<RectTransform>().sizeDelta = new Vector2(55,55);
        return inp;
    }
    public void Display(int i){
        int c = AllItem.Count;
        //Debug.Log(c);
        for(i=0;i<c;i++){
            Destroy(AllItem[0]);
            AllItem.Remove(AllItem[0]);
        }
        inputID = workshop.GetComponent<WorshopScript>().inputID;
        int l = inputID.Count;
        resize(l);
        if(i>=l)i=l-1;
        if(i<0)i=0;
        for(i=0;i<l;i++){
            //Debug.Log("i = "+i);
            AllItem.Add(CreateItem(inputID[i]));
            AllItem[i].GetComponent<RectTransform>().localPosition = new Vector2(174,-40-i*(diff_y+size_y));
            AllItem[i] = mini(AllItem[i]);

            ReNum(AllItem[i],workshop.GetComponent<WorshopScript>().inputAmount[i]);
        }
    }
    public void RemoveItem(int _id,GameObject element){
        workshop.GetComponent<WorshopScript>().Remove(_id);
        inventory.GetComponent<inventoryScript>().AddItem(_id,1);
    }
    public void reset(){
        int c = AllItem.Count;
        //Debug.Log(c);
        for(int i=0;i<c;i++){
            Destroy(AllItem[0]);
            AllItem.Remove(AllItem[0]);
        }
    }


}
