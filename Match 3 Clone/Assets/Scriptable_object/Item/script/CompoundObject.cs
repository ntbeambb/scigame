using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="new Compound object", menuName ="Inventory System/Item/Compound")]
public class CompoundObject : ItemObject
{
    public void Awake(){
        type = Itemtype.Compound;
    }
}
