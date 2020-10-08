using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            //Debug.Log("escape clicked");
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale =1f;
        GameIsPaused = false;
    }
    public void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale =0f;
        GameIsPaused = true;
    }
    public void Menu(){
    	string path = Application.dataPath +"/Scripts/UI/numberOfHearts.txt";
        File.WriteAllText(path,"5");
        //Debug.Log("launchStartMenu");
        HeartHealthVisual.heartHealthSystemStatic.Damage(4*40);
        HeartHealthVisual.heartHealthSystemStatic.Heal(20);
        SceneManager.LoadScene("StartingMenu");
        Debug.Log("StartingMenu launched");
        Time.timeScale =1f;
        GameIsPaused = false;
    }
    public void Quit(){
        bool editionMode = false;
        if (editionMode)
        {
            string path = Application.dataPath +"/Scripts/UI/numberOfHearts.txt";
            HeartHealthVisual.heartHealthSystemStatic.Damage(4*40);
            HeartHealthVisual.heartHealthSystemStatic.Heal(20);
            //Debug.Log("Quit clicked");
            File.WriteAllText(path,"5");
            SceneManager.LoadScene("StartingMenu");
            Time.timeScale =1f;
            GameIsPaused = false;
        }
        else
        {
            Application.Quit();
        }
    }
}
