using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadGeneration : MonoBehaviour
{
    public GameObject m_BreadPrefab;
    public int m_Freq;

    private int cpt;
    private Random rand = new Random();

    // Start is called before the first frame update
    void Start()
    {
        cpt = m_Freq;
    }

    // Update is called once per frame
    void Update()
    {
        if (cpt == m_Freq)
        {
            float x = Random.Range(-220, 220);
            GameObject m_Bread = Instantiate(m_BreadPrefab, transform.position, transform.rotation);
            m_Bread.transform.Translate(new Vector3(x, 0, 0));
            m_Bread.transform.localScale = new Vector3(1f, 1f, 1f);
            cpt = 0;
        }
        else
        {
            cpt += 1;
        }
    }
}
