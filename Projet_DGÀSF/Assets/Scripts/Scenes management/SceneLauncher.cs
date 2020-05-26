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

    public void LaunchCharacterChoice()
    {
        SceneManager.LoadScene("CharacterChoice");
    }

    public void LaunchFirstLevelAutiste()
    {
        SceneManager.LoadScene("FirstLevelAutiste");
    }

    public void LaunchFirstLevelBipolaire()
    {
        SceneManager.LoadScene("FirstLevelBipolaire");
    }

    public void LaunchFirstLevelFibro()
    {
        SceneManager.LoadScene("FirstLevelFibro");
    }
}
