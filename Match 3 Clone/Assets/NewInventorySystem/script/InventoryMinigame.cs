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
    private int StartPo;
    private GameObject[] DisplaySlot;
    private Vector2 BarPo;
    public float StartX;
    private TextMeshPro textmesh;
    void Start(){
        StartPo = 0;
        DisplaySlot = new GameObject[Slot];
    }
    private GameObject AddInventory(int po,GameObject gob)
    {
        Vector2 tilepo = new Vector2(transform.position.x + po*1.82f - StartX,transform.position.y);
		GameObject tile = Instantiate(gob,tilepo, Quaternion.identity) as GameObject;
        tile.transform.parent = this.transform;
        tile.name = "Inventory "+po;
        return tile;
    }
    private GameObject findgameobject(int _id){
        int co = ItemList.Count;
        int i;
        for(i=0;i<co;i++){
            if(ItemList[i].GetComponent<InventoryDot>().id == _id){
                return ItemList[i];
            }
        }
        return null;
    }
    public void display(){
        int count = backpack.Count();
        for(int i=0;i<Slot;i++){
            if(DisplaySlot[i]!=null)Destroy(DisplaySlot[i]);
        }
        for(int i=0; i < Slot && i+StartPo < count; i++){
            //Debug.Log("BUG "+i);
            GameObject tempgameobject = findgameobject(backpack.container[i+StartPo].id);
            int num=backpack.container[i+StartPo].amount;
            DisplaySlot[i] = AddInventory(i,tempgameobject);
            //Show Volume of Item
            
                GameObject ItemAmount =DisplaySlot[i].GetComponent<InventoryDot>().Number;
               textmesh = ItemAmount.GetComponent<TextMeshPro>();
               textmesh.text = "X "+num.ToString();
            
            //Debug.Log("Show element "+tempgameobject.name+" with "+num);

        }
    }
    public void Slide(int inp){
        if(inp == 0){
            if(StartPo>0)StartPo--;
        }else if(inp == 1){
            if(StartPo+Slot<backpack.Count())StartPo++;
        }
        display();
    }
}
