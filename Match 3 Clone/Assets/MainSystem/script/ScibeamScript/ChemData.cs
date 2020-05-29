using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Chem_Data", menuName = "Problem/ChemData")]
public class ChemData : ScriptableObject
{
    [SerializeField] public List<ChemEq> chemdata = new List<ChemEq>();
}
[System.Serializable]
public class ChemEq{
    [SerializeField] public List<slot> input = new List<slot>();
    [SerializeField] public List<slot> output = new List<slot>();
}
