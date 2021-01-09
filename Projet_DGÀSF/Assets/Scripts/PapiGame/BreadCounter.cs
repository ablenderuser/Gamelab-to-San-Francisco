using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BreadCounter : MonoBehaviour
{
    private int cpt = 0;
    private GameObject m_Score;
    private GameObject m_Time;
    private float m_RealTimeCounter = 0;

    public int m_IntTimeCounter;

    // Start is called before the first frame update
    void Start()
    {
        m_Score = GameObject.Find("Score");
        m_Time = GameObject.Find("Time");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(cpt);
        m_RealTimeCounter += 1;
        if (m_RealTimeCounter == 75)
        {
            if (m_IntTimeCounter == 1)
            {
                SceneManager.LoadScene("SecondLevelFibro");
                for (int i=0; i<cpt; i++)
                {
                    GameObject.Find("Player character").GetComponent<Inventory>().GiveItem("Bout de pain");
                }
                Debug.Log(GameObject.Find("Player character").GetComponent<Inventory>().InInventory("Bout de pain"));
            }
            else
            {
                m_IntTimeCounter -= 1;
                m_RealTimeCounter = 0;
            }
        }
        m_Time.GetComponent<Text>().text = m_IntTimeCounter.ToString();

    }

    public void Add()
    {
        cpt += 1;
        m_Score.GetComponent<Text>().text = cpt.ToString();
        //m_Text.SetActive(true);
        /*for(int i=0; i<100000; i++)
        {
            int a = 0;
        }
        m_Text.SetActive(false);*/
    }
}
