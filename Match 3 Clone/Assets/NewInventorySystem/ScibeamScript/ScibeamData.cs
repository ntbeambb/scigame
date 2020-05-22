using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScibeamData : MonoBehaviour
{
    [SerializeField] 
    private int id;
    [SerializeField] 
    private string SciName;

    public int ID(){
        return id;
    }
}
