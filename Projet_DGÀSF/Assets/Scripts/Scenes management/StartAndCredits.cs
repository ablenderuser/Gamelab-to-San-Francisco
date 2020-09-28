using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartAndCredits
{

    public void StartScene()
    {
        SceneManager.LoadScene("CharacterChoice");
    }

    public void CreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }

}
