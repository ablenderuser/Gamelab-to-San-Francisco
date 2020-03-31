using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLauncher : MonoBehaviour
{
    public void LaunchStartingMenu()
    {
        SceneManager.LoadScene("StartingMenu");
    }

    public void LaunchCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void LaunchFirstLevel()
    {
        SceneManager.LoadScene("FirstLevel");
    }
}
