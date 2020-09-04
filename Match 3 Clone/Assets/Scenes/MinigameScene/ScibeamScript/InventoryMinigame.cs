using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryMinigame : MonoBehaviour
{
    private int nextpo;
    public InventorySystem backpack;
    public List<GameObject> ItemList = new List<GameObject>();
    public int Slot;
    public int StartPo;
    private GameObject[] DisplaySlot;
    private GameObject[] NumSlot;
    private Vector2 BarPo;
    public float StartX;
    private TextMeshPro textmesh;
    public GameObject ShowNum;
    void Awake(){
        StartPo = 0;
        DisplaySlot = new GameObject[Slot];
        NumSlot = new GameObject[Slot];
    }
    private GameObject AddInventory(int po,GameObject gob)
    {
        Vector2 tilepo = new Vector2(transform.position.x + po*1.82f - StartX,transform.position.y);
		GameObject tile = Instantiate(gob,tilepo, Quaternion.identity) as GameObject;
        tile.transform.parent = this.transform;
        tile.name = "Inventory "+po;
        return tile;
    }
    private GameObject AddNumber(int num){
        int ck = 0;
        if(num>9)ck++;
        if(num>99)ck++;
        Vector2 tilepo = new Vector2(transform.position.x + 1.1f - 0.6f*ck,transform.position.y);
		GameObject NumDis = Instantiate(ShowNum,tilepo, Quaternion.identity,this.transform) as GameObject;
        textmesh = NumDis.GetComponent<TextMeshPro>();
        textmesh.text = "X "+ num.ToString();
       // NumDis.transform.parent = this.transform;
        NumDis.name = "Item Amount";
        return NumDis;
    }
    private GameObject findgameobject(int _id){
        int co = ItemList.Count;
        int i;
        for(i=0;i<co;i++){
            if(ItemList[i].GetComponent<ScibeamData>().ID() == _id){
                return ItemList[i];
            }
        }
        return null;
    }
    public void display(){
        int count = backpack.container.Count;
        //Debug.Log("Count = "+count);
        Slot = DisplaySlot.Length;
        for(int i=0; i<Slot; i++){
            if(DisplaySlot[i]!=null)Destroy(DisplaySlot[i]);
            if(NumSlot[i]!=null)Destroy(NumSlot[i]);
        }
        for(int i=0; i < Slot && i+StartPo < count; i++){
            
            //Debug.Log("BUG "+i);
            GameObject tempgameobject = findgameobject(backpack.container[i+StartPo].id);
            //if(tempgameobject == null)Debug.Log("Bug Null");
            int num=backpack.container[i+StartPo].amount;
            DisplaySlot[i] = AddInventory(i,tempgameobject);
            //Show Volume of Item
            NumSlot[i] = AddNumber(num);
            
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
