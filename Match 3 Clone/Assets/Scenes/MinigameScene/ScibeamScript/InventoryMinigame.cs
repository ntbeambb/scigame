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
    private TextMeshPro textmesh;
    public GameObject gob;

    public GameObject ShowNum;
    public float Itemsize;
    void Awake(){
        StartPo = 0;
        DisplaySlot = new GameObject[Slot];
        NumSlot = new GameObject[Slot];
        

    }
    void Start(){
        //Debug.Log("start");
        //display();

        //bug
        var po = DisplaySlot[0].GetComponent<Transform>().position;
        po.x = 2.121f;
        po.z += 10;
        DisplaySlot[0].GetComponent<Transform>().position = po;
            //Debug.Log("item Po "+DisplaySlot[0].GetComponent<Transform>().position);
        po = NumSlot[0].GetComponent<Transform>().position;
        po.z += 10;
        NumSlot[0].GetComponent<Transform>().position = po;

    }


    private GameObject AddInventory(int po,int _id)
    {
        //set start position
        Vector2 tilepo = new Vector2(transform.position.x + po*1.82f - StartX,transform.position.y);
        //create gameobject
		GameObject tile = Instantiate(gob,tilepo, Quaternion.identity) as GameObject;
        //insert image
        tile.GetComponent<Image>().sprite = allpic.ID[_id];
        //insert scibaem data
        tile.GetComponent<ScibeamData>().id = _id;
        //resize
        var sx = allpic.ID[_id].bounds.size.x / Itemsize;
        var sy = allpic.ID[_id].bounds.size.y / Itemsize;
        tile.GetComponent<RectTransform>().localScale = new Vector2(sx,sy);
        //relocate
        var pox = tile.GetComponent<RectTransform>().position;
        pox.x += (sx-1f)/2;
        tile.GetComponent<RectTransform>().position = pox;
        //set parent
        tile.transform.SetParent(transform);
        //rename
        tile.name = "Inventory "+po;
        return tile;
    }
    private GameObject AddNumber(int num){
        int ck = (int)Mathf.Log10(num);
        Vector2 tilepo = new Vector2(transform.position.x + 1.1f - 0.6f*ck,transform.position.y);
		GameObject NumDis = Instantiate(ShowNum,tilepo, Quaternion.identity,this.transform) as GameObject;
        textmesh = NumDis.GetComponent<TextMeshPro>();
        textmesh.text = "X "+ num.ToString();
       // NumDis.transform.parent = this.transform;
        NumDis.name = "Item Amount";
        return NumDis;
    }
    /*private GameObject findgameobject(int _id){
        int co = ItemList.Count;
        int i;
        for(i=0;i<co;i++){
            if(ItemList[i].GetComponent<ScibeamData>().ID() == _id){
                return ItemList[i];
            }
        }
        return null;
    }*/
    public void display(){
        //Debug.Log("Display");
        int count = backpack.container.Count;
        //Debug.Log("Count = "+count);
        Slot = DisplaySlot.Length;
        for(int i=0; i<Slot; i++){
            if(DisplaySlot[i]!=null)Destroy(DisplaySlot[i]);
            if(NumSlot[i]!=null)Destroy(NumSlot[i]);
        }
        for(int i=0; i < Slot && i+StartPo < count; i++){
            
            //Debug.Log("BUG "+i);
            //GameObject tempgameobject = findgameobject(backpack.container[i+StartPo].id);
            //if(tempgameobject == null)Debug.Log("Bug Null");
            int num=backpack.container[i+StartPo].amount;
            DisplaySlot[i] = AddInventory(i,backpack.container[i+StartPo].id);
            //Show Volume of Item
            NumSlot[i] = AddNumber(num);
            //float zz = DisplaySlot[i].GetComponent<Transform>().position.x;
            //Debug.Log("Po z "+zz);
            //Debug.Log("Show element "+tempgameobject.name+" with "+num);

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
