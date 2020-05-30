using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Dialog;
using System.IO;

public class NPCController : MonoBehaviour
{
    public Object m_JsonFile;
    public int m_Heal;
    public int m_Damage;
    private bool m_HealUsed = false;
    private bool m_DamageUsed = false;

    public GameObject m_InvisibleObject;

    private Dialog m_Dialog;

    private bool m_Possible = true;
    private bool m_Interacting = false;
    private bool m_StartDialog = false;
    private bool m_EndDialog = false;

    // Start is called before the first frame update
    void Start()
    {
        string jsonString = File.ReadAllText(Application.streamingAssetsPath + "/" + m_JsonFile.name);
        m_Dialog = CreateFromJSON(jsonString);
    }

    public void SetEndDialog()
    {
        m_EndDialog = true;
        m_StartDialog = false;
    }

    public void SetImpossible()
    {
        m_Possible = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (m_Possible && new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).magnitude == 0 && !m_StartDialog && !m_EndDialog)
        {
            m_Interacting = true;
            GetComponent<DialogManager>().StartDialog(m_Dialog);
            if (m_InvisibleObject != null)
            {
                m_InvisibleObject.SetActive(true); //Afficher l'objet invisible
            }
            m_StartDialog = true;
        }

        if (m_Heal != null && m_HealUsed == false) //Enlever ou ajouter des vies
		{
			HeartHealthVisual.heartHealthSystemStatic.Heal(4*m_Heal);
            m_HealUsed = true;
		}
        if (m_Damage != null && m_DamageUsed == false)
        {
            HeartHealthVisual.heartHealthSystemStatic.Damage(4*m_Damage);
            m_DamageUsed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (m_Possible && m_Interacting)
        {
            GetComponent<DialogManager>().EndDialog();
            m_StartDialog = false;
            m_Interacting = false;
            m_EndDialog = false;
        }
    }
}
