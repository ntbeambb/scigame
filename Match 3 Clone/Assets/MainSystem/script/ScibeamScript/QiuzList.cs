using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New QuizProblem", menuName = "Problem/QuizList")]
public class QiuzList : ScriptableObject
{
    public List<QuizProblem> Qlist;
}
