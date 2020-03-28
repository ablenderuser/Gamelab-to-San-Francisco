using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Dialog;
using System.IO;

public class NPC : MonoBehaviour
{
    public Object m_JsonFile;
    public string m_Action;
    public GameObject m_CoAnimatedObject;

    private bool m_IsObject = false;
    private Animator m_Animator;
    private Animator m_CoAnimator;
    private Dialog m_Dialog;

    private bool m_Interacting = false;
    private bool m_StartDialog = false;
    private bool m_EndDialog = false;

    // Start is called before the first frame update
    void Start()
    {
        if (m_JsonFile != null)
        {
            string jsonString = File.ReadAllText(Application.streamingAssetsPath + "/" + m_JsonFile.name);
            m_Dialog = CreateFromJSON(jsonString);
            m_IsObject = false;
        }
        if (m_Action != null)
        {
            m_Animator = GetComponent<Animator>();
            if (m_CoAnimatedObject != null)
            {
                m_CoAnimator = m_CoAnimatedObject.GetComponent<Animator>();
            }
            m_IsObject = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEndDialog()
    {
        m_EndDialog = true;
        m_StartDialog = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).magnitude == 0 && !m_StartDialog && !m_EndDialog)
        {
            m_Interacting = true;
            if (m_IsObject)
            {
                m_Animator.SetBool(m_Action, true);
                if (m_CoAnimatedObject != null)
                {
                    m_CoAnimator.SetBool(m_Action, true);
                }
            }
            else
            {
                GetComponent<DialogManager>().StartDialog(m_Dialog);
                m_StartDialog = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (m_Interacting)
        {
            if (m_IsObject)
            {
                m_Animator.SetBool(m_Action, false);
                if (m_CoAnimatedObject != null)
                {
                    m_CoAnimator.SetBool(m_Action, false);
                }
            }
            else
            {
                GetComponent<DialogManager>().EndDialog();
                m_StartDialog = false;
                m_Interacting = false;
                m_EndDialog = false;
            }
        }
    }
}
