using UnityEngine;
using static Dialog;
using System.IO;

public class NPCController : MonoBehaviour
{
    public string m_JsonFile_Start;
    public string m_JsonFile_During;
    public string m_JsonFile_End;

    private int m_Step;
    public int m_Heal;
    public int m_Damage;
    //private bool m_HealUsed = false;
    //private bool m_DamageUsed = false;

    public GameObject m_InvisibleObject;

    private Dialog m_Dialog;
    private Dialog m_Dialog_Start;
    private Dialog m_Dialog_During;
    private Dialog m_Dialog_End;

    private bool m_Possible = true;
    private bool m_Interacting = false;
    private bool m_StartDialog = false;
    private bool m_EndDialog = false;
    private bool m_End = false;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("StreamingAssets PATH : " + Application.streamingAssetsPath);
        // Debug.Log("JSON file name : " + m_JsonFile);
        if (m_JsonFile_Start != null)
        {
            string path = Path.Combine(Application.streamingAssetsPath, m_JsonFile_Start);
            Debug.Log(m_JsonFile_Start);
            Debug.Log("JSON PATH : " + path);
            string jsonString = File.ReadAllText(path);
            m_Dialog_Start = CreateFromJSON(jsonString);
            m_Dialog = m_Dialog_Start;
            m_Step = 1;
        }

        if (m_JsonFile_During != null)
        {
            string path_during = Path.Combine(Application.streamingAssetsPath, m_JsonFile_During);
            Debug.Log(m_JsonFile_During);
            Debug.Log("JSON PATH : " + path_during);
            string jsonStringDuring = File.ReadAllText(path_during);
            m_Dialog_During = CreateFromJSON(jsonStringDuring);
        }

        if (m_JsonFile_End != null)
        {
            string path_end = Path.Combine(Application.streamingAssetsPath, m_JsonFile_End);
            Debug.Log(m_JsonFile_End);
            Debug.Log("JSON PATH : " + path_end);
            string jsonStringEnd = File.ReadAllText(path_end);
            m_Dialog_End = CreateFromJSON(jsonStringEnd);
        }
    }

    public void SetEndDialog()
    {
        // S'il y a un dialogue durant la quête
        if (m_Dialog_During != null && m_Step == 1) {
            m_Dialog = m_Dialog_During;
            m_Step = 2;
        // S'il y a un dialogue de fin
        } else if (m_Dialog_End != null && (m_Step == 1 || m_Step == 2)) {
            m_Dialog = m_Dialog_End;
            m_Step = 3;
        } else {
            m_EndDialog = true;
        }
        m_StartDialog = false;
    }

    public void SetImpossible()
    {
        m_Possible = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if  (m_End)
        {
            return;
        }

        // Debug.Log(GameObject.Find("Player character").GetComponent<PlayerController>().IsInteracting());

        if (m_Possible && new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).magnitude == 0 && !m_StartDialog && !m_EndDialog && m_Dialog != null && !GameObject.Find("Player character").GetComponent<PlayerController>().IsInteracting())
        {
            m_Interacting = true;
            GameObject.Find("Player character").GetComponent<PlayerController>().SetInteracting(true);
            GetComponent<DialogManager>().StartDialog(m_Dialog);
            m_StartDialog = true;
        }

        if (m_EndDialog)
        {
            HeartHealthVisual.heartHealthSystemStatic.Heal(m_Heal);
            HeartHealthVisual.heartHealthSystemStatic.Damage(m_Damage);

            m_End = true;
            if (m_InvisibleObject != null)
            {
                m_InvisibleObject.SetActive(true); //Afficher l'objet invisible
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject.Find("Player character").GetComponent<PlayerController>().SetInteracting(false);

        if (m_Possible && m_Interacting)
        {
            GetComponent<DialogManager>().EndDialog(false);
            m_StartDialog = false;
            m_Interacting = false;
            m_EndDialog = false;
        }
    }
}
