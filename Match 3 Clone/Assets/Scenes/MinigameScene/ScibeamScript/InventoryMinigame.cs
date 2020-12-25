using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryMinigame : MonoBehaviour
{
    private int nextpo;
    public InventorySystem backpack;
    //public List<GameObject> ItemList = new List<GameObject>();
    public int Slot;
    public int StartPo;
    public AllpicID allpic;
    private GameObject[] DisplaySlot;
    private GameObject[] NumSlot;
    private Vector2 BarPo;
    public float StartX;
    public float StNum;
    private TextMeshPro textmesh;
    public GameObject gob;

    public GameObject ShowNum;
    public float Itemsize;
    void Start(){
        StartPo = 0;
        DisplaySlot = new GameObject[Slot];
        NumSlot = new GameObject[Slot];
        display();
    }
    private GameObject AddInventory(int po,int _id)
    {
        //set start position
        Vector2 tilepo = new Vector2(transform.position.x + po*1.82f ,transform.position.y);
        //create gameobject
		GameObject tile = Instantiate(gob,tilepo, Quaternion.identity,transform) as GameObject;
        //insert image
        tile.GetComponent<Image>().sprite = allpic.ID[_id];
        //insert scibaem data
        tile.GetComponent<ScibeamData>().id = _id;
        //resize
        var sx = allpic.ID[_id].bounds.size.x * Itemsize;
        //var sy = allpic.ID[_id].bounds.size.y * Itemsize;
        tile.GetComponent<RectTransform>().localScale = new Vector2(sx,sx);
        //relocate
        var temp = new Vector2(tile.transform.localPosition.x + sx/2 + StartX ,tile.transform.localPosition.y);
        tile.transform.localPosition = temp;
        //rename
        tile.name = "Inventory "+po;
        return tile;
    }
    private GameObject AddNumber(int num){
        Vector2 tilepo = new Vector2(transform.position.x+StNum,transform.position.y);
		GameObject NumDis = Instantiate(ShowNum,tilepo, Quaternion.identity,transform) as GameObject;
        textmesh = NumDis.GetComponent<TextMeshPro>();
        textmesh.text = "X "+ num.ToString();
        NumDis.name = "Item Amount";
        return NumDis;
    }
    public void display(){
        int count = backpack.container.Count;
        Slot = DisplaySlot.Length;
        for(int i=0; i<Slot; i++){
            if(DisplaySlot[i]!=null)Destroy(DisplaySlot[i]);
            if(NumSlot[i]!=null)Destroy(NumSlot[i]);
        }
        if(StartPo < 0){
            StartPo = 0;
            return;}
        for(int i=0; i < Slot && i+StartPo < count; i++){
            int num=backpack.container[i+StartPo].amount;
            DisplaySlot[i] = AddInventory(i,backpack.container[i+StartPo].id);
            //Show Volume of Item
            NumSlot[i] = AddNumber(num);
        }

    }
    public void Slide(int inp){
        if(inp == 0){
            if(StartPo>0)StartPo--;
        }else if(inp == 1){
            if(StartPo+Slot<backpack.container.Count)StartPo++;
        }
        display();
    }
}
