using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField]private SceneDataScript SceneData;
    public void SettingScene(){
        if(SceneData.NumScene>0 && SceneData.PreScene()=="setting")BackScene();
        else{
            SceneData.PushScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("setting");
        }
        
    }
    public void MinigameScene(){
        if(SceneData.NumScene>0 && SceneData.PreScene()=="minigame")BackScene();
        else{
            SceneData.PushScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("minigame");
        }
    }
    public void WorkshopScene(){
        if(SceneData.NumScene>0 && SceneData.PreScene()=="workshop")BackScene();
        else{
            SceneData.PushScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("workshop");
        }
        
    }
    public void MainmanuScene(){
        if(SceneData.NumScene>0 && SceneData.PreScene()=="mainmanu")BackScene();
        else{
            SceneData.PushScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("mainmanu");
        }
        
    }
    public void GraphScene(){
        if(SceneData.NumScene>0 && SceneData.PreScene()=="TreeGraph")BackScene();
        else{
            SceneData.PushScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("TreeGraph");
        }
        
    }
    public void BackScene(){
         SceneManager.LoadScene(SceneData.PopScene());
    }
    public void Reset(){
        SceneData.ResetScene();
    }
    public void SendMission(int _id){
        SceneData.IdMission = _id;
    }
}
