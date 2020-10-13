using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizPlay : MonoBehaviour
{
    public QiuzList quizList;
    public SceneDataScript mem;
    public GraphData Gdata;
    public GameObject popup;
    public GameObject quiz;
    [SerializeField] private QuizProblem Qnow;
    public List<GameObject> Ctext = new List<GameObject>();
    public void StartQuiz(){

        if(Random.Range(0.00f,1.00f)>mem.Qprob){
            End();
            mem.SkipCo++;
            mem.Qprob += (1.00f-mem.Qprob)*((float)mem.SkipCo/(float)mem.LockQ);
            if(mem.Qprob>1.00f)mem.Qprob=1.00f;
            //Debug.Log((float)mem.SkipCo/(float)mem.LockQ);
            return;
        }
        mem.SkipCo=0;
        mem.Qprob = mem.StartQuizProb;
        Qnow = quizList.Qlist[Gdata.Pass[Random.Range(0,Gdata.Pass.Count)]];
        for(var i=0;i<4;i++){
            TextMeshProUGUI textmesh = Ctext[i].GetComponent<TextMeshProUGUI>();
            //Debug.Log("Start "+subtask.SubText);
            textmesh.text = Qnow.choice[i];
        }
        quiz.SetActive(true);


    }
    public void Answer(int inp){
        if(inp==Qnow.correct){
            Debug.Log("correct");
        }else Debug.Log("False");
        End();
    }
    private void End(){
        quiz.SetActive(false);
        popup.SetActive(true);
    }
}
