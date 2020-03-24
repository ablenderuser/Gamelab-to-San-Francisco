using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Dialog;
using System.IO;

public class NPC : MonoBehaviour
{
    public Object m_JsonFile;
    private Dialog m_Dialog;

    // Start is called before the first frame update
    void Start()
    {
        string jsonString = File.ReadAllText(Application.streamingAssetsPath + "/" + m_JsonFile.name);
        m_Dialog = CreateFromJSON(jsonString);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<DialogManager>().StartDialog(m_Dialog);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponent<DialogManager>().EndDialog();
    }
}
