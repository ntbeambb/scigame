using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New QuizProblem", menuName = "Problem/Quiz")]
public class QuizProblem : ScriptableObject
{
    public string text;
    public List<string> choice;
    public int id;
    public int correct;
    [Header("Prize")]
    public int PrizeID;
    public int PrizeAmount;
}
