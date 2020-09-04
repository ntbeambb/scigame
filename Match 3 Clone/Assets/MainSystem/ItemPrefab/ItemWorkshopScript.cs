using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorkshopScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void SendId()
    {
        GameObject workshop = GameObject.Find("workshop");
        int id = GetComponent<ScibeamData>().ID();
        workshop.GetComponent<WorshopScript>().GetItem(id);

    }
}
