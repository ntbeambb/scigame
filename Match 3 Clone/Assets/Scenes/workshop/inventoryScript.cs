using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryScript : MonoBehaviour
{
    public GameObject parent;
    public AllpicID Allpic;
    public GameObject prefab;
    public List<GameObject> AllItem;
    public InventorySystem Backpack;

    public float sty;
    public float muly;
    private GameObject CreateItem(int _id){
        Vector2 po = new Vector2(0,0);
        GameObject ret = Instantiate(prefab,po,Quaternion.identity,parent.transform) as GameObject;
        ret.GetComponent<ScibeamData>().id = _id;
        //add image
        return ret;

    }
    void Start()
    {
        int i;
        int l = Backpack.container.Count;
        for(i=0;i<l;i++){
            AllItem.Add(CreateItem(Backpack.container[i].id));
            AllItem[i].transform.position = new Vector2(0,i*muly+sty);
            // change number
        }
    }


    void Update()
    {
        
    }
}
