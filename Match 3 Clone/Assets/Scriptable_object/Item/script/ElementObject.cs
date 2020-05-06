using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="new Element object", menuName ="Inventory System/Item/Element")]
public class ElementObject : ItemObject
{
    public int ElementNumber;
    public void Awake(){
        type = Itemtype.Element;
    }
}
