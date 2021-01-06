using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorkshopScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject text;
    public GameObject image;
    public void SendId()
    {   
        if(transform.parent.name == "Slot"){
            GameObject inventory = GameObject.Find("Slot");
            int id = GetComponent<ScibeamData>().ID();
            inventory.GetComponent<inventoryScript>().RemoveItem(id,this.gameObject);
        }else{
            GameObject inventory = GameObject.Find("Content");
            int id = GetComponent<ScibeamData>().ID();
            inventory.GetComponent<InputMix>().RemoveItem(id);

        }

    }
}
