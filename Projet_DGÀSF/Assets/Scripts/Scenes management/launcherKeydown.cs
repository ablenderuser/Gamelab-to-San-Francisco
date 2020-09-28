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

    private GameObject m_Image;

    public void Start()
    {
        m_Image = GameObject.Find("Image");
        //m_StartButton = GameObject.Find("Start");
        //m_CreditsButton = GameObject.Find("Credits");
    }

    public void Update(){
        if(Input.anyKeyDown){
            m_Image.GetComponent<Animator>().SetBool("Up", true);
            //m_Image.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
            m_StartButton.SetActive(true);
            m_CreditsButton.SetActive(true);
        }
    }
}
