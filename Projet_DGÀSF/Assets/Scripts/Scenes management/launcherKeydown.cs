using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class launcherKeydown : MonoBehaviour
{
    public string NextScene;

    public void Update(){
        if(Input.anyKeyDown){
            SceneManager.LoadScene(NextScene);
        }
    }
}
