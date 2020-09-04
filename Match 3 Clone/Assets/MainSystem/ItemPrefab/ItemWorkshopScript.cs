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
        GameObject inventory = GameObject.Find("Slot");
        int id = GetComponent<ScibeamData>().ID();
        inventory.GetComponent<inventoryScript>().RemoveItem(id,this.gameObject);

    }
}
