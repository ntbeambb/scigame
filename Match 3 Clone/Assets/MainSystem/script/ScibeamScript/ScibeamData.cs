using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScibeamData : MonoBehaviour
{
    [SerializeField] 
    public int id;
    [SerializeField] 
    public string SciName;

    public int ID(){
        return id;
    }
}
