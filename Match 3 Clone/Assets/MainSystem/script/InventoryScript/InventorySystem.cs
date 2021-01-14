using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Inventory", menuName = "New Inventory System/inventory")]
public class InventorySystem : ScriptableObject
{
    //start Debug
    public List<slot> container = new List<slot>();
    //end Debug
    private int IdCheck(int _id){
        int ma = container.Count;
        for(int i=0; i < ma;i++){
            if(container[i].id == _id)return i;
        }
        return -1;
    }
    public void AddItem(int _id,string _name, int _amount){
        int inp = IdCheck(_id);
        if(inp==-1){
            container.Add(new slot(_id,_name,_amount));
            return;
        }
        else{
            container[inp].amount += _amount;
        }
    }
    public int RemoveItem(int _id, int _amount){
        int inp = IdCheck(_id);
        if(inp==-1 || container[inp].amount<_amount)return 0;
        container[inp].amount -= _amount;
        if(container[inp].amount == 0){
            container.Remove(container[inp]);
            return 2;
        }
        return 1;
    }  
    public void Save(){
        SaveSystem.SaveInventory(this);
    }
    public void Load(){
        SaveSystem.LoadInventory(this);
    }
    public void Divide(){
        int i = 0;
        for(i=0;i<container.Count;i++){
            container[i].amount = container[i].amount/2;
            if(container[i].amount == 0){
                container.Remove(container[i]);
                i--;
            }
        }
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
    public slot Copy(){
        slot ret = new slot(this.id,this.name,this.amount);
        return ret;
    }
}
