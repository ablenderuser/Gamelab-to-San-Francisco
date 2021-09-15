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
    	string path = Path.Combine(Application.persistentDataPath, "numberOfHearts.txt");
        if (File.Exists(path)) {
            File.WriteAllText(path, HeartHealthVisual.heartHealthSystemStatic.GetNumberOfHearts().ToString());
        }
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
            string path = Path.Combine(Application.persistentDataPath, "numberOfHearts.txt");
            if (File.Exists(path)) {
                File.WriteAllText(path, HeartHealthVisual.heartHealthSystemStatic.GetNumberOfHearts().ToString());
            }
            HeartHealthVisual.heartHealthSystemStatic.Damage(5);
            HeartHealthVisual.heartHealthSystemStatic.Heal(1);
            //Debug.Log("Quit clicked");
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
