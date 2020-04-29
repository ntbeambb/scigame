using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    public void MainScene(){
        SceneManager.LoadScene("mainmanu");

    }
    public void settingScene(){
        SceneManager.LoadScene("setting");

    }
    public void minigameScene(){
        SceneManager.LoadScene("minigame");

    }
    public void workshopScene(){
        SceneManager.LoadScene("workshop");

    }

}
