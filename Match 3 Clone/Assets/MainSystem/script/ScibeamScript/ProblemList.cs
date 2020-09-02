using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ProblemList", menuName = "Problem/Problemlist")]
public class ProblemList : ScriptableObject
{
    public List<ProblemData> Plist = new List<ProblemData>();
}
