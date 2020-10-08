using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class launcherKeydown : MonoBehaviour
{
    public string NextScene;
    public GameObject m_StartButton;
    public GameObject m_CreditsButton;
    public GameObject m_MoreAboutButton;
    public GameObject m_OptionsButton;

    private GameObject m_Image;

    public void Start()
    {
        m_Image = GameObject.Find("Image");
    }

    public void Update(){
        if(Input.anyKeyDown){
            m_Image.GetComponent<Animator>().SetBool("Up", true);

            m_StartButton.SetActive(true);
            m_CreditsButton.SetActive(true);
            m_MoreAboutButton.SetActive(true);
            m_OptionsButton.SetActive(true);
        }
    }
}
