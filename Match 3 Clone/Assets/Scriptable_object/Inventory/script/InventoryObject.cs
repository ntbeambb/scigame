using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> Backpack = new List<InventorySlot>();
    public void AddItem(ItemObject _item,int _amount){
        bool check=false;
        int NumItem = Backpack.Count;
        for(int i = 0; i<NumItem; i++)
        {
            if(Backpack[i].item == _item){
                Backpack[i].AddAmount(_amount);
                check = true;
                break;
            }

        }
        if(!check){
            Backpack.Add(new InventorySlot( _item, _amount));
        }
    }
}

[System.Serializable]
public class InventorySlot{
    public ItemObject item;
    public int amount;
    public InventorySlot(ItemObject _item,int _amount){
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value){
        amount += value;
    }

}