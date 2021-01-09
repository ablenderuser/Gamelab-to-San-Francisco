using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadCatch : MonoBehaviour
{
    private BreadCounter m_CharacterCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        m_CharacterCounter = GameObject.Find("Player character").GetComponent<BreadCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Attrapé");
        m_CharacterCounter.Add();
        Destroy(gameObject);
    }
}
