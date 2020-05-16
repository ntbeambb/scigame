using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Inventory", menuName = "New Inventory System/inventory")]
public class InventorySystem : ScriptableObject
{
    private int[] Backpack = new int[100];
    private string[] AllName = new string[100];
    private int count = 0;
    //start Debug
    public List<slot> container = new List<slot>();
    private void display(){
        container.Clear();
        for(int i=0;i<100;i++){
            if(Backpack[i]>0){
                container.Add(new slot(i,AllName[i],Backpack[i]));
            }
        }
    }
    //end Debug
    private bool IdCheck(int _id){
        if(_id<=0||_id>=100){
            Debug.Log("Valid id");
            return false;
        } 
        return true;
    }
    public void Reset(){
        for(int i = 0 ;i<100;i++){
            Backpack[i] = 0;
        }
        count = 0;
        
        display();
    }
    public void AddItem(int _id,string _name, int _amount){
        if(!IdCheck(_id))return;
        if(Backpack[_id] == 0 && _amount > 0)count++;
        Backpack[_id]+=_amount;
        AllName[_id]=_name;
        
        display();
    }
    public void RemoveItem(int _id, int _amount){
        if(!IdCheck(_id))return;
        if(Backpack[_id]-_amount>=0 && _amount >= 0){
            Backpack[_id]-=_amount;
            if(Backpack[_id] == 0)count--;
        }

        display();
    }
    public int Count(){
        return count;
    }
    public int CheckItem(int _id){
        return Backpack[_id];
    }

}
//Debug part
[System.Serializable]
public class slot{
    public int id;
    public string name;
    public int amount;
    public slot(int _id,string _name,int _amount){
        id=_id;
        amount=_amount;
        name=_name;
    }
}
